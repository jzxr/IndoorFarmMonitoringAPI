namespace IndoorFarmMonitoringAPI.Tests;

using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IndoorFarmMonitoringAPI.Data;
using IndoorFarmMonitoringAPI.Services;

public class PlantDataServiceTests
{
    [Fact]
    public async Task GetCombinedDataAsync_ReturnsMergedResults()
    {
        // Arrange
        var httpClient = new HttpClient();

        var configValues = new Dictionary<string, string>
        {
            { "ConnectionStrings:DefaultConnection", "Host=localhost;Port=5432;Database=farm_db;Username=postgres;Password=farm_pass" }
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configValues!)
            .Build();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestFarmDB")
            .Options;

        var context = new AppDbContext(options);

        var logger = LoggerFactory.Create(builder => builder.AddConsole())
                                  .CreateLogger<PlantDataService>();

        var service = new PlantDataService(httpClient, configuration, context, logger);

        // Act
        var result = await service.GetCombinedDataAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result); 
    }
}