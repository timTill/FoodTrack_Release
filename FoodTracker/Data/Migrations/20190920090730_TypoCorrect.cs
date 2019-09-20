using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodTracker.Data.Migrations
{
    public partial class TypoCorrect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "priority",
                table: "Foods",
                newName: "Priority");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "Foods",
                newName: "priority");
        }
    }
}
