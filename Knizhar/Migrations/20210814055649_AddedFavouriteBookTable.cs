using Microsoft.EntityFrameworkCore.Migrations;

namespace Knizhar.Migrations
{
    public partial class AddedFavouriteBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookUser",
                columns: table => new
                {
                    FavouriteBooksId = table.Column<int>(type: "int", nullable: false),
                    FavouriteBooksId1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookUser", x => new { x.FavouriteBooksId, x.FavouriteBooksId1 });
                    table.ForeignKey(
                        name: "FK_BookUser_AspNetUsers_FavouriteBooksId1",
                        column: x => x.FavouriteBooksId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookUser_Books_FavouriteBooksId",
                        column: x => x.FavouriteBooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteBooks",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteBooks", x => new { x.BookId, x.UserId });
                    table.ForeignKey(
                        name: "FK_FavouriteBooks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookUser_FavouriteBooksId1",
                table: "BookUser",
                column: "FavouriteBooksId1");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteBooks_UserId",
                table: "FavouriteBooks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookUser");

            migrationBuilder.DropTable(
                name: "FavouriteBooks");
        }
    }
}
