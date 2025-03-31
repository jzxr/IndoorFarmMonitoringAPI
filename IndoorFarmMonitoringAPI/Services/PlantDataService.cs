namespace IndoorFarmMonitoringAPI.Services;

using System;
using IndoorFarmMonitoringAPI.Data;
using IndoorFarmMonitoringAPI.Models;

public class PlantDataService : IPlantDataService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly AppDbContext _context;
    private readonly ILogger<PlantDataService> _logger;

    public PlantDataService(HttpClient httpClient, IConfiguration config, AppDbContext context, ILogger<PlantDataService> logger)
    {
        _httpClient = httpClient;
        _config = config;
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<CombinedPlantData>> GetCombinedDataAsync()
    {
        _logger.LogInformation("Fetching sensor data...");
        var sensorStart = DateTime.Now;
        var sensorResponse = await _httpClient.GetFromJsonAsync<List<SensorData>>("http://3.0.148.231:8010/sensor-readings");
        _logger.LogInformation("Sensor data fetched in {Time}ms", (DateTime.Now - sensorStart).TotalMilliseconds);

        _logger.LogInformation("Fetching plant configuration...");
        var configStart = DateTime.Now;
        var configResponse = await _httpClient.GetFromJsonAsync<List<PlantConfiguration>>("http://3.0.148.231:8020/plant-configurations");
        _logger.LogInformation("Plant config fetched in {Time}ms", (DateTime.Now - configStart).TotalMilliseconds);

        if (sensorResponse == null || configResponse == null)
        {
            _logger.LogError("One or both external API responses returned null.");
            throw new Exception("Failed to retrieve data from external APIs.");
        }


        var combined = from sensor in sensorResponse
                       join config in configResponse on sensor.Tray_Id equals config.Tray_Id
                       select new CombinedPlantData
                       {
                           Tray_Id = sensor.Tray_Id,
                           ActualTemperature = sensor.Temperature,
                           TargetTemperature = config.TargetTemperature,
                           ActualHumidity = sensor.Humidity,
                           TargetHumidity = config.TargetHumidity,
                           ActualLight = sensor.Light,
                           TargetLight = config.TargetLight
                       };

        foreach (var item in combined)
        {
            if (!item.IsTemperatureOk)
            {
                item.Alerts.Add($"Temperature mismatch: Actual {item.ActualTemperature}°C vs Target {item.TargetTemperature}°C");
            }

            if (!item.IsHumidityOk)
            {
                item.Alerts.Add($"Humidity mismatch: Actual {item.ActualHumidity}% vs Target {item.TargetHumidity}%");
            }

            if (!item.IsLightOk)
            {
                item.Alerts.Add($"Light mismatch: Actual {item.ActualLight} vs Target {item.TargetLight}");
            }

            if (item.Alerts.Any())
            {
                _logger.LogWarning("Tray {TrayId} has alerts:\n{Alerts}", item.Tray_Id, string.Join("\n", item.Alerts));
            }

            if (item.OutOfRangeMetrics.Any())
            {
                _logger.LogWarning("Tray {TrayId} out of range metrics: {Metrics}", item.Tray_Id, string.Join(", ", item.OutOfRangeMetrics));
            }

            _context.PlantData.Add(new PlantDataEntity
            {
                Tray_Id = item.Tray_Id,
                ActualTemperature = item.ActualTemperature,
                TargetTemperature = item.TargetTemperature,
                ActualHumidity = item.ActualHumidity,
                TargetHumidity = item.TargetHumidity,
                ActualLight = item.ActualLight,
                TargetLight = item.TargetLight
            });
        }

        await _context.SaveChangesAsync();

        return combined;
    }
}