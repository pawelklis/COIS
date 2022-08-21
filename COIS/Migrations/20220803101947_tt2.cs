using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coisCore.Migrations
{
    public partial class tt2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceClass",
                table: "DeviceTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceClass",
                table: "DeviceTypes");
        }
    }
}
