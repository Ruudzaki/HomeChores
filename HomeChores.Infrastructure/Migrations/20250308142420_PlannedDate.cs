using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeChores.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PlannedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedDate",
                table: "Chores",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlannedDate",
                table: "Chores");
        }
    }
}
