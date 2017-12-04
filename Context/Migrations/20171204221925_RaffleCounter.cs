using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Context.Migrations
{
    public partial class RaffleCounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cicle",
                table: "Raffles",
                newName: "Cicle");

            migrationBuilder.AddColumn<int>(
                name: "RaffleCounter",
                table: "Raffles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RaffleCounter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Counter = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaffleCounter", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaffleCounter");

            migrationBuilder.DropColumn(
                name: "RaffleCounter",
                table: "Raffles");

            migrationBuilder.RenameColumn(
                name: "Cicle",
                table: "Raffles",
                newName: "cicle");
        }
    }
}
