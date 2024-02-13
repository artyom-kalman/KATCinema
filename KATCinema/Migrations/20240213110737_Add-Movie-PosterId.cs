using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KATCinema.Migrations
{
    /// <inheritdoc />
    public partial class AddMoviePosterId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Poster",
                table: "Movies",
                newName: "PosterUrl");

            migrationBuilder.AddColumn<string>(
                name: "PosterId",
                table: "Movies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosterId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "PosterUrl",
                table: "Movies",
                newName: "Poster");
        }
    }
}
