using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IBlogWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddLastUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "BlogPosts",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "BlogPosts");
        }
    }
}
