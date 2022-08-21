using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coisCore.Migrations
{
    public partial class dd1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PassDescription",
                table: "Scanners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassDescription",
                table: "Requisites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassDescription",
                table: "Forklifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassDescription",
                table: "Batteries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassDescription",
                table: "Scanners");

            migrationBuilder.DropColumn(
                name: "PassDescription",
                table: "Requisites");

            migrationBuilder.DropColumn(
                name: "PassDescription",
                table: "Forklifts");

            migrationBuilder.DropColumn(
                name: "PassDescription",
                table: "Batteries");
        }
    }
}
