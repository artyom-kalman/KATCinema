using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KATCinema.Migrations
{
    /// <inheritdoc />
    public partial class RefatorModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowNumber",
                table: "ReservedSeats");

            migrationBuilder.DropColumn(
                name: "SeatNumber",
                table: "ReservedSeats");

            migrationBuilder.DropColumn(
                name: "TotalRows",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "TotalSeats",
                table: "Halls");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RowNumber",
                table: "ReservedSeats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeatNumber",
                table: "ReservedSeats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalRows",
                table: "Halls",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalSeats",
                table: "Halls",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
