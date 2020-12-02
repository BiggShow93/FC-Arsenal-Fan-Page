using Microsoft.EntityFrameworkCore.Migrations;

namespace ArsenalFanPage.Data.Migrations
{
    public partial class RemovedTitleByCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
