using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalrTracking_Test.Data.Migrations
{
    public partial class editTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AspNetUsers_UserId1",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Vehicles",
                newName: "ApplicationUserId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Vehicles",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_UserId1",
                table: "Vehicles",
                newName: "IX_Vehicles_ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AspNetUsers_ApplicationUserId1",
                table: "Vehicles",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AspNetUsers_ApplicationUserId1",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId1",
                table: "Vehicles",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Vehicles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_ApplicationUserId1",
                table: "Vehicles",
                newName: "IX_Vehicles_UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AspNetUsers_UserId1",
                table: "Vehicles",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
