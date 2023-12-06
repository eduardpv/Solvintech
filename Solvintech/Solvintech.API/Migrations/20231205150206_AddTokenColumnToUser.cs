﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solvintech.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenColumnToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "ApplicationUsers");
        }
    }
}
