﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Knizhar.Data.Migrations
{
    public partial class AddPropertyCreatedOnToBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "Books");
        }
    }
}
