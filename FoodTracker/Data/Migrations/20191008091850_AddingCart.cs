using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodTracker.Data.Migrations
{
    public partial class AddingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInCart",
                table: "Foods",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInCart",
                table: "Foods");
        }
    }
}
