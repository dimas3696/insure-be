using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Insure.Migrations
{
    public partial class ChangeTimeFormatInDaySchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "DaySchedules");

            migrationBuilder.DropColumn(
                name: "To",
                table: "DaySchedules");

            migrationBuilder.AddColumn<int>(
                name: "FromHour",
                table: "DaySchedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FromMinute",
                table: "DaySchedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToHour",
                table: "DaySchedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToMinute",
                table: "DaySchedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromHour",
                table: "DaySchedules");

            migrationBuilder.DropColumn(
                name: "FromMinute",
                table: "DaySchedules");

            migrationBuilder.DropColumn(
                name: "ToHour",
                table: "DaySchedules");

            migrationBuilder.DropColumn(
                name: "ToMinute",
                table: "DaySchedules");

            migrationBuilder.AddColumn<DateTime>(
                name: "From",
                table: "DaySchedules",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "To",
                table: "DaySchedules",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
