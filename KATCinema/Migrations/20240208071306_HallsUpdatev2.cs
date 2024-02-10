using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KATCinema.Migrations
{
    /// <inheritdoc />
    public partial class HallsUpdatev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservedSeats_Seat_SeatId",
                table: "ReservedSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_Row_Halls_HallId",
                table: "Row");

            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Row_RowId",
                table: "Seat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seat",
                table: "Seat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Row",
                table: "Row");

            migrationBuilder.RenameTable(
                name: "Seat",
                newName: "Seats");

            migrationBuilder.RenameTable(
                name: "Row",
                newName: "Rows");

            migrationBuilder.RenameIndex(
                name: "IX_Seat_RowId",
                table: "Seats",
                newName: "IX_Seats_RowId");

            migrationBuilder.RenameIndex(
                name: "IX_Row_HallId",
                table: "Rows",
                newName: "IX_Rows_HallId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rows",
                table: "Rows",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedSeats_Seats_SeatId",
                table: "ReservedSeats",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_Halls_HallId",
                table: "Rows",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Rows_RowId",
                table: "Seats",
                column: "RowId",
                principalTable: "Rows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservedSeats_Seats_SeatId",
                table: "ReservedSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_Halls_HallId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Rows_RowId",
                table: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rows",
                table: "Rows");

            migrationBuilder.RenameTable(
                name: "Seats",
                newName: "Seat");

            migrationBuilder.RenameTable(
                name: "Rows",
                newName: "Row");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_RowId",
                table: "Seat",
                newName: "IX_Seat_RowId");

            migrationBuilder.RenameIndex(
                name: "IX_Rows_HallId",
                table: "Row",
                newName: "IX_Row_HallId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seat",
                table: "Seat",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Row",
                table: "Row",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedSeats_Seat_SeatId",
                table: "ReservedSeats",
                column: "SeatId",
                principalTable: "Seat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Row_Halls_HallId",
                table: "Row",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Row_RowId",
                table: "Seat",
                column: "RowId",
                principalTable: "Row",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
