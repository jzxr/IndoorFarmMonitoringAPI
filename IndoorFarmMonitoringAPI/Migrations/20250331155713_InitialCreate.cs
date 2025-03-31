using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IndoorFarmMonitoringAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlantData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrayId = table.Column<string>(type: "text", nullable: false),
                    ActualTemperature = table.Column<float>(type: "real", nullable: false),
                    TargetTemperature = table.Column<float>(type: "real", nullable: false),
                    ActualHumidity = table.Column<float>(type: "real", nullable: false),
                    TargetHumidity = table.Column<float>(type: "real", nullable: false),
                    ActualLight = table.Column<float>(type: "real", nullable: false),
                    TargetLight = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlantData");
        }
    }
}
