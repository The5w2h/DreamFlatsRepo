using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamFlats.Migrations
{
    /// <inheritdoc />
    public partial class SeedFlatTableWithCreatedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flats",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 7, 18, 53, 11, 916, DateTimeKind.Local).AddTicks(4200));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flats",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
