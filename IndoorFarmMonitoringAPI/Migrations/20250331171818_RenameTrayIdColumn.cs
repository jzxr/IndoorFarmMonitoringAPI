using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndoorFarmMonitoringAPI.Migrations
{
    public partial class RenameTrayIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlantData",
                table: "PlantData");

            migrationBuilder.DropColumn(
                name: "TrayId",
                table: "PlantData");

            migrationBuilder.RenameTable(
                name: "PlantData",
                newName: "plant_data");

            migrationBuilder.AddColumn<int>(
                name: "tray_id",
                table: "plant_data",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_plant_data",
                table: "plant_data",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_plant_data",
                table: "plant_data");

            migrationBuilder.DropColumn(
                name: "tray_id",
                table: "plant_data");

            migrationBuilder.RenameTable(
                name: "plant_data",
                newName: "PlantData");

            migrationBuilder.AddColumn<string>(
                name: "TrayId",
                table: "PlantData",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlantData",
                table: "PlantData",
                column: "Id");
        }
    }
}
