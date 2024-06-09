using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SystemLogger.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    PK_PhoneNumber = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.PK_PhoneNumber);
                });

            migrationBuilder.CreateTable(
                name: "WebSitesNames",
                columns: table => new
                {
                    PK_Website = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ServerStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    FK_PhoneNumbers = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSitesNames", x => x.PK_Website);
                });

            migrationBuilder.CreateTable(
                name: "ErorrLogs",
                columns: table => new
                {
                    PK_ErrorLog = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WebSiteName = table.Column<string>(type: "TEXT", nullable: false),
                    ErrorCode = table.Column<string>(type: "TEXT", nullable: false),
                    StartOfError = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndOfError = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WebsiteId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErorrLogs", x => x.PK_ErrorLog);
                    table.ForeignKey(
                        name: "FK_ErorrLogs_WebSitesNames_WebsiteId",
                        column: x => x.WebsiteId,
                        principalTable: "WebSitesNames",
                        principalColumn: "PK_Website",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbersWebsite",
                columns: table => new
                {
                    PhoneNumbersPK_PhoneNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    WebsitesPK_Website = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbersWebsite", x => new { x.PhoneNumbersPK_PhoneNumber, x.WebsitesPK_Website });
                    table.ForeignKey(
                        name: "FK_PhoneNumbersWebsite_PhoneNumbers_PhoneNumbersPK_PhoneNumber",
                        column: x => x.PhoneNumbersPK_PhoneNumber,
                        principalTable: "PhoneNumbers",
                        principalColumn: "PK_PhoneNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhoneNumbersWebsite_WebSitesNames_WebsitesPK_Website",
                        column: x => x.WebsitesPK_Website,
                        principalTable: "WebSitesNames",
                        principalColumn: "PK_Website",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErorrLogsPhoneNumbers",
                columns: table => new
                {
                    ErorrLogsId = table.Column<int>(type: "INTEGER", nullable: false),
                    PhoneNumbersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErorrLogsPhoneNumbers", x => new { x.ErorrLogsId, x.PhoneNumbersId });
                    table.ForeignKey(
                        name: "FK_ErorrLogsPhoneNumbers_ErorrLogs_ErorrLogsId",
                        column: x => x.ErorrLogsId,
                        principalTable: "ErorrLogs",
                        principalColumn: "PK_ErrorLog",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ErorrLogsPhoneNumbers_PhoneNumbers_PhoneNumbersId",
                        column: x => x.PhoneNumbersId,
                        principalTable: "PhoneNumbers",
                        principalColumn: "PK_PhoneNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PhoneNumbers",
                columns: new[] { "PK_PhoneNumber", "Name", "PhoneNumber" },
                values: new object[] { 1, "Contact1", "0913*******" });

            migrationBuilder.InsertData(
                table: "WebSitesNames",
                columns: new[] { "PK_Website", "FK_PhoneNumbers", "Name", "ServerStatus", "Url" },
                values: new object[,]
                {
                    { 1, 0, "Fadia", true, "https://fadiashop.com/wakeup" },
                    { 2, 0, "Varzesh3", true, "https://www.varzesh3.com/" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ErorrLogs_WebsiteId",
                table: "ErorrLogs",
                column: "WebsiteId");

            migrationBuilder.CreateIndex(
                name: "IX_ErorrLogsPhoneNumbers_PhoneNumbersId",
                table: "ErorrLogsPhoneNumbers",
                column: "PhoneNumbersId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbersWebsite_WebsitesPK_Website",
                table: "PhoneNumbersWebsite",
                column: "WebsitesPK_Website");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErorrLogsPhoneNumbers");

            migrationBuilder.DropTable(
                name: "PhoneNumbersWebsite");

            migrationBuilder.DropTable(
                name: "ErorrLogs");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "WebSitesNames");
        }
    }
}
