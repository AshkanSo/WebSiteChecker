using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemLogger.Migrations
{
    /// <inheritdoc />
    public partial class thirdmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WebSitesNames",
                keyColumn: "PK_Website",
                keyValue: 2,
                column: "Url",
                value: "https://localhost:44304/api/TestUrl/Test");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WebSitesNames",
                keyColumn: "PK_Website",
                keyValue: 2,
                column: "Url",
                value: "http://localhost:8000/");
        }
    }
}
