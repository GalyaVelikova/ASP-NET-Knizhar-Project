using Microsoft.EntityFrameworkCore.Migrations;

namespace Knizhar.Data.Migrations
{
    public partial class AddConditionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConditionId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Conditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConditionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_ConditionId",
                table: "Books",
                column: "ConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Conditions_ConditionId",
                table: "Books",
                column: "ConditionId",
                principalTable: "Conditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Conditions_ConditionId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Conditions");

            migrationBuilder.DropIndex(
                name: "IX_Books_ConditionId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ConditionId",
                table: "Books");
        }
    }
}
