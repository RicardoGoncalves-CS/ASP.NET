using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcAddNewField.Migrations
{
    // In this migration a new column 'Rating' is being added to the Movies Table.
    // This method makes only the addition of a Rating column without affecting the other columns in the table.

    /// <inheritdoc />
    public partial class Rating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Movie");
        }
    }
}
