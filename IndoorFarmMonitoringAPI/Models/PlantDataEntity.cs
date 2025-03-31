using System.ComponentModel.DataAnnotations.Schema;

namespace IndoorFarmMonitoringAPI.Models
{
    [Table("plant_data")]
    public class PlantDataEntity
	{
        public int Id { get; set; }
        [Column("tray_id")]
        public int Tray_Id { get; set; }
        public float ActualTemperature { get; set; }
        public float TargetTemperature { get; set; }
        public float ActualHumidity { get; set; }
        public float TargetHumidity { get; set; }
        public float ActualLight { get; set; }
        public float TargetLight { get; set; }
    }
}

