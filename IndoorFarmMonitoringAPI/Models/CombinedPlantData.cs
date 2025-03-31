namespace IndoorFarmMonitoringAPI.Data;

public class CombinedPlantData
{
    public int Tray_Id { get; set; }

    public float ActualTemperature { get; set; }
    public float TargetTemperature { get; set; }
    public bool IsTemperatureOk => Math.Abs(ActualTemperature - TargetTemperature) <= 2;

    public float ActualHumidity { get; set; }
    public float TargetHumidity { get; set; }
    public bool IsHumidityOk => Math.Abs(ActualHumidity - TargetHumidity) <= 5;

    public float ActualLight { get; set; }
    public float TargetLight { get; set; }
    public bool IsLightOk => Math.Abs(ActualLight - TargetLight) <= 50;

    public List<string> Alerts { get; set; } = new();

    public string Status => (IsTemperatureOk && IsHumidityOk && IsLightOk)
        ? "All metrics within range"
        : "Some metrics out of range";

    public List<string> OutOfRangeMetrics
    {
        get
        {
            var list = new List<string>();
            if (!IsTemperatureOk) list.Add("temperature");
            if (!IsHumidityOk) list.Add("humidity");
            if (!IsLightOk) list.Add("light");
            return list;
        }
    }
}

