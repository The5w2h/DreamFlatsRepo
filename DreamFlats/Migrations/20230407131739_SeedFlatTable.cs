using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamFlats.Migrations
{
    /// <inheritdoc />
    public partial class SeedFlatTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Flats",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "ModifiedDate", "Name", "Occupancy", "Rate", "SquareFeet" },
                values: new object[] { 1, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem epsum", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Royal View", 5, 200.0, 550 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flats",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
