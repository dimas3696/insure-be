using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Insure.Migrations
{
    public partial class DeleteWeekScheduleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaySchedules_WeekSchedules_WeekScheduleId",
                table: "DaySchedules");

            migrationBuilder.DropTable(
                name: "WeekSchedules");

            migrationBuilder.RenameColumn(
                name: "WeekScheduleId",
                table: "DaySchedules",
                newName: "OfficeId");

            migrationBuilder.RenameIndex(
                name: "IX_DaySchedules_WeekScheduleId",
                table: "DaySchedules",
                newName: "IX_DaySchedules_OfficeId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Offices",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Offices",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Offices",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DaySchedules_Offices_OfficeId",
                table: "DaySchedules",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaySchedules_Offices_OfficeId",
                table: "DaySchedules");

            migrationBuilder.RenameColumn(
                name: "OfficeId",
                table: "DaySchedules",
                newName: "WeekScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_DaySchedules_OfficeId",
                table: "DaySchedules",
                newName: "IX_DaySchedules_WeekScheduleId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Offices",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Offices",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Offices",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "WeekSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OfficeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeekSchedules_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeekSchedules_OfficeId",
                table: "WeekSchedules",
                column: "OfficeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DaySchedules_WeekSchedules_WeekScheduleId",
                table: "DaySchedules",
                column: "WeekScheduleId",
                principalTable: "WeekSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
