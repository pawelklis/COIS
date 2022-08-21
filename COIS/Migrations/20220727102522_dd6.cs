using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coisCore.Migrations
{
    public partial class dd6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceDamages_Employees_EmployeeId",
                table: "DeviceDamages");

            migrationBuilder.DropIndex(
                name: "IX_DeviceDamages_EmployeeId",
                table: "DeviceDamages");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "DeviceDamages");

            migrationBuilder.AddColumn<string>(
                name: "Employee",
                table: "DeviceDamages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "DeviceDamages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Employee",
                table: "DeviceDamages");

            migrationBuilder.DropColumn(
                name: "User",
                table: "DeviceDamages");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "DeviceDamages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDamages_EmployeeId",
                table: "DeviceDamages",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceDamages_Employees_EmployeeId",
                table: "DeviceDamages",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
