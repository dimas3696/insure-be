using Microsoft.EntityFrameworkCore.Migrations;

namespace Insure.Migrations
{
    public partial class ReworkManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorNew");

            migrationBuilder.DropTable(
                name: "NewTag");

            migrationBuilder.CreateTable(
                name: "AuthorNews",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    NewId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorNews", x => new { x.NewId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_AuthorNews_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorNews_News_NewId",
                        column: x => x.NewId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagNews",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    NewId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagNews", x => new { x.NewId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TagNews_News_NewId",
                        column: x => x.NewId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagNews_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorNews_AuthorId",
                table: "AuthorNews",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_TagNews_TagId",
                table: "TagNews",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorNews");

            migrationBuilder.DropTable(
                name: "TagNews");

            migrationBuilder.CreateTable(
                name: "AuthorNew",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "integer", nullable: false),
                    NewsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorNew", x => new { x.AuthorsId, x.NewsId });
                    table.ForeignKey(
                        name: "FK_AuthorNew_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorNew_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewTag",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "integer", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewTag", x => new { x.NewsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_NewTag_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorNew_NewsId",
                table: "AuthorNew",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_NewTag_TagsId",
                table: "NewTag",
                column: "TagsId");
        }
    }
}
