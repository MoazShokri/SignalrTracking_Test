﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalrTracking_Test.Data.Migrations
{
    public partial class EditRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AspNetUsers_ApplicationUserId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_ApplicationUserId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Vehicles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ApplicationUserId",
                table: "Vehicles",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AspNetUsers_ApplicationUserId",
                table: "Vehicles",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
