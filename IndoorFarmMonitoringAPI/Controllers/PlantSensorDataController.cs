using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class PlantSensorDataController : ControllerBase
{
    private readonly IPlantDataService _plantDataService;

    public PlantSensorDataController(IPlantDataService plantDataService)
    {
        _plantDataService = plantDataService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await _plantDataService.GetCombinedDataAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}