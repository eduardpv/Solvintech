using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solvintech.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTokenColumnToAccessToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "ApplicationUsers",
                newName: "AccessToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccessToken",
                table: "ApplicationUsers",
                newName: "Token");
        }
    }
}
