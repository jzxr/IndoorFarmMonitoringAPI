namespace IndoorFarmMonitoringAPI.Data;

public class PlantConfiguration
{
    public int Tray_Id { get; set; }
    public float TargetTemperature { get; set; }
    public float TargetHumidity { get; set; }
    public float TargetLight { get; set; }
}