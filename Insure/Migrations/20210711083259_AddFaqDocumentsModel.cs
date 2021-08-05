using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Insure.Migrations
{
    public partial class AddFaqDocumentsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FaqsDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DownloadLink = table.Column<string>(type: "text", nullable: true),
                    FaqId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaqsDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaqsDocuments_Faqs_FaqId",
                        column: x => x.FaqId,
                        principalTable: "Faqs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FaqsDocuments_FaqId",
                table: "FaqsDocuments",
                column: "FaqId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaqsDocuments");
        }
    }
}
