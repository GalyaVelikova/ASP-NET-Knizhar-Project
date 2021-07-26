using Microsoft.EntityFrameworkCore.Migrations;

namespace Knizhar.Data.Migrations
{
    public partial class RemoveCommentPropertyFromConditionsTableAndAddItToBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Conditions");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Books",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Conditions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
