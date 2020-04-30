using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AtelierAuto.Data.Migrations
{
    public partial class Ora : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Ora",
                table: "Appointment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ora",
                table: "Appointment");
        }
    }
}
