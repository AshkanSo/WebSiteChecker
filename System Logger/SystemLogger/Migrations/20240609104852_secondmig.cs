using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemLogger.Migrations
{
    /// <inheritdoc />
    public partial class secondmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PhoneNumbers",
                keyColumn: "PK_PhoneNumber",
                keyValue: 1,
                column: "PhoneNumber",
                value: "09131111111");

            migrationBuilder.InsertData(
                table: "PhoneNumbers",
                columns: new[] { "PK_PhoneNumber", "Name", "PhoneNumber" },
                values: new object[] { 2, "Contact1", "09132222222" });

            migrationBuilder.UpdateData(
                table: "WebSitesNames",
                keyColumn: "PK_Website",
                keyValue: 1,
                column: "FK_PhoneNumbers",
                value: 1);

            migrationBuilder.UpdateData(
                table: "WebSitesNames",
                keyColumn: "PK_Website",
                keyValue: 2,
                columns: new[] { "FK_PhoneNumbers", "Name", "Url" },
                values: new object[] { 2, "LocalHost", "http://localhost:8000/" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PhoneNumbers",
                keyColumn: "PK_PhoneNumber",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "PhoneNumbers",
                keyColumn: "PK_PhoneNumber",
                keyValue: 1,
                column: "PhoneNumber",
                value: "0913*******");

            migrationBuilder.UpdateData(
                table: "WebSitesNames",
                keyColumn: "PK_Website",
                keyValue: 1,
                column: "FK_PhoneNumbers",
                value: 0);

            migrationBuilder.UpdateData(
                table: "WebSitesNames",
                keyColumn: "PK_Website",
                keyValue: 2,
                columns: new[] { "FK_PhoneNumbers", "Name", "Url" },
                values: new object[] { 0, "Varzesh3", "https://www.varzesh3.com/" });
        }
    }
}
