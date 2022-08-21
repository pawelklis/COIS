using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coisCore.Migrations
{
    public partial class tt1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceClass",
                table: "Scanners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeviceClass",
                table: "Requisites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeviceClass",
                table: "Forklifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeviceClass",
                table: "Batteries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceClass",
                table: "Scanners");

            migrationBuilder.DropColumn(
                name: "DeviceClass",
                table: "Requisites");

            migrationBuilder.DropColumn(
                name: "DeviceClass",
                table: "Forklifts");

            migrationBuilder.DropColumn(
                name: "DeviceClass",
                table: "Batteries");
        }
    }
}
