using Microsoft.EntityFrameworkCore.Migrations;

namespace Knizhar.Data.Migrations
{
    public partial class KnizharAndTownsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConditionName",
                table: "Conditions",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "KnizharId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KnizharId1",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Knizhari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<int>(type: "int", maxLength: 25, nullable: false),
                    TownId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knizhari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Knizhari_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Knizhari_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_KnizharId",
                table: "Books",
                column: "KnizharId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_KnizharId1",
                table: "Books",
                column: "KnizharId1");

            migrationBuilder.CreateIndex(
                name: "IX_Knizhari_TownId",
                table: "Knizhari",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_Knizhari_UserId",
                table: "Knizhari",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Knizhari_KnizharId",
                table: "Books",
                column: "KnizharId",
                principalTable: "Knizhari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Knizhari_KnizharId1",
                table: "Books",
                column: "KnizharId1",
                principalTable: "Knizhari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Knizhari_KnizharId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Knizhari_KnizharId1",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Knizhari");

            migrationBuilder.DropTable(
                name: "Towns");

            migrationBuilder.DropIndex(
                name: "IX_Books_KnizharId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_KnizharId1",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "KnizharId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "KnizharId1",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "ConditionName",
                table: "Conditions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);
        }
    }
}
