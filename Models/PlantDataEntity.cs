using System;
namespace IndoorFarmMonitoringAPI.Models
{
	public class PlantDataEntity
	{
        public int Id { get; set; }
        public string TrayId { get; set; }
        public float ActualTemperature { get; set; }
        public float TargetTemperature { get; set; }
        public float ActualHumidity { get; set; }
        public float TargetHumidity { get; set; }
        public float ActualLight { get; set; }
        public float TargetLight { get; set; }
    }
}

