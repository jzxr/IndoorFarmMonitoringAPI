using IndoorFarmMonitoringAPI.Data;

public interface IPlantDataService
{
    Task<IEnumerable<CombinedPlantData>> GetCombinedDataAsync();
}