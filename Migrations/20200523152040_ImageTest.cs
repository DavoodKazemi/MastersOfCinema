using Microsoft.EntityFrameworkCore.Migrations;

namespace MastersOfCinema.Migrations
{
    public partial class ImageTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoURL",
                table: "Director",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoURL",
                table: "Director");
        }
    }
}
