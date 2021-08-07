using Microsoft.EntityFrameworkCore.Migrations;

namespace Knizhar.Migrations
{
    public partial class RemoveColumnImagePathFromImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Images");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
