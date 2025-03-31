public class CombinedPlantData
{
    public string TrayId { get; set; } = string.Empty;
    public float ActualTemperature { get; set; }
    public float TargetTemperature { get; set; }
    public bool IsTemperatureOk => Math.Abs(ActualTemperature - TargetTemperature) <= 2;

    public float ActualHumidity { get; set; }
    public float TargetHumidity { get; set; }
    public bool IsHumidityOk => Math.Abs(ActualHumidity - TargetHumidity) <= 5;

    public float ActualLight { get; set; }
    public float TargetLight { get; set; }
    public bool IsLightOk => Math.Abs(ActualLight - TargetLight) <= 50;
}