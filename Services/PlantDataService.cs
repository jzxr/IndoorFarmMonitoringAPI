using System;
using IndoorFarmMonitoringAPI.Data;
using IndoorFarmMonitoringAPI.Models;

public class PlantDataService : IPlantDataService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly AppDbContext _context;

    public PlantDataService(HttpClient httpClient, IConfiguration config, AppDbContext context)
    {
        _httpClient = httpClient;
        _config = config;
        _context = context;
    }

    public async Task<IEnumerable<CombinedPlantData>> GetCombinedDataAsync()
    {
        var sensorResponse = await _httpClient.GetFromJsonAsync<List<SensorData>>("http://3.0.148.231:8010/sensor-readings");
        var configResponse = await _httpClient.GetFromJsonAsync<List<PlantConfiguration>>("http://3.0.148.231:8020/plant-configurations");

        if (sensorResponse == null || configResponse == null)
            throw new Exception("Failed to fetch or parse API data.");

        var combined = from sensor in sensorResponse
                       join config in configResponse on sensor.TrayId equals config.TrayId
                       select new CombinedPlantData
                       {
                           TrayId = sensor.TrayId,
                           ActualTemperature = sensor.Temperature,
                           TargetTemperature = config.TargetTemperature,
                           ActualHumidity = sensor.Humidity,
                           TargetHumidity = config.TargetHumidity,
                           ActualLight = sensor.Light,
                           TargetLight = config.TargetLight
                       };

        foreach (var item in combined)
        {
            _context.PlantData.Add(new PlantDataEntity
            {
                TrayId = item.TrayId,
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