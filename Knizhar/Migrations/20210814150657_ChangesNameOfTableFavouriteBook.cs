using Microsoft.EntityFrameworkCore.Migrations;

namespace Knizhar.Migrations
{
    public partial class ChangesNameOfTableFavouriteBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookUser_AspNetUsers_FavouriteBooksId1",
                table: "BookUser");

            migrationBuilder.RenameColumn(
                name: "FavouriteBooksId1",
                table: "BookUser",
                newName: "UserFavouriteBooksId");

            migrationBuilder.RenameIndex(
                name: "IX_BookUser_FavouriteBooksId1",
                table: "BookUser",
                newName: "IX_BookUser_UserFavouriteBooksId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookUser_AspNetUsers_UserFavouriteBooksId",
                table: "BookUser",
                column: "UserFavouriteBooksId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookUser_AspNetUsers_UserFavouriteBooksId",
                table: "BookUser");

            migrationBuilder.RenameColumn(
                name: "UserFavouriteBooksId",
                table: "BookUser",
                newName: "FavouriteBooksId1");

            migrationBuilder.RenameIndex(
                name: "IX_BookUser_UserFavouriteBooksId",
                table: "BookUser",
                newName: "IX_BookUser_FavouriteBooksId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BookUser_AspNetUsers_FavouriteBooksId1",
                table: "BookUser",
                column: "FavouriteBooksId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
