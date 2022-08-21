using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coisCore.Migrations
{
    public partial class db3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zones_Users_UserTypeId",
                table: "Zones");

            migrationBuilder.DropIndex(
                name: "IX_Zones_UserTypeId",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "Zones");

            migrationBuilder.AddColumn<string>(
                name: "Zones",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zones",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "Zones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zones_UserTypeId",
                table: "Zones",
                column: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zones_Users_UserTypeId",
                table: "Zones",
                column: "UserTypeId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
