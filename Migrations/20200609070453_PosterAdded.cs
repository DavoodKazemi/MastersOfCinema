using Microsoft.EntityFrameworkCore.Migrations;

namespace MastersOfCinema.Migrations
{
    public partial class PosterAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MoviePosterName",
                table: "Movie",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoviePosterName",
                table: "Movie");
        }
    }
}
