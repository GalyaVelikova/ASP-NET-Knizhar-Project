using Microsoft.EntityFrameworkCore.Migrations;

namespace Knizhar.Migrations
{
    public partial class RemoveForeignKeysToFavouriteBookTableFromUserAndBookTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FavouriteBooks_FavouriteBookId1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_FavouriteBooks_FavouriteBookId1",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_FavouriteBookId1",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FavouriteBookId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FavouriteBookId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "FavouriteBookId1",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "FavouriteBookId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FavouriteBookId1",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FavouriteBookId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FavouriteBookId1",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavouriteBookId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FavouriteBookId1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_FavouriteBookId1",
                table: "Books",
                column: "FavouriteBookId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FavouriteBookId1",
                table: "AspNetUsers",
                column: "FavouriteBookId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FavouriteBooks_FavouriteBookId1",
                table: "AspNetUsers",
                column: "FavouriteBookId1",
                principalTable: "FavouriteBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_FavouriteBooks_FavouriteBookId1",
                table: "Books",
                column: "FavouriteBookId1",
                principalTable: "FavouriteBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
