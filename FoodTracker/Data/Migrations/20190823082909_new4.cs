using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodTracker.Data.Migrations
{
    public partial class new4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Foods");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BestBefore",
                table: "Foods",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BestBefore",
                table: "Foods",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "Foods",
                nullable: true);
        }
    }
}
