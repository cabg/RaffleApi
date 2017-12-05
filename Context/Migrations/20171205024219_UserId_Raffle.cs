using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Context.Migrations
{
    public partial class UserId_Raffle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Raffles_Users_UserId",
                table: "Raffles");

            migrationBuilder.DropIndex(
                name: "IX_Raffles_UserId",
                table: "Raffles");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Raffles",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Raffles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Raffles_UserId",
                table: "Raffles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Raffles_Users_UserId",
                table: "Raffles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
