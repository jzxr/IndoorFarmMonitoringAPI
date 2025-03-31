public interface IPlantDataService
{
    Task<IEnumerable<CombinedPlantData>> GetCombinedDataAsync();
}