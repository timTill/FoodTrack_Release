using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodTracker.Data.Migrations
{
    public partial class PriorityAsEnumToFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "priority",
                table: "Foods",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "priority",
                table: "Foods");
        }
    }
}
