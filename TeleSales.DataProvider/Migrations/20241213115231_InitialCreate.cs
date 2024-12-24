using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleSales.DataProvider.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kanals",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kanals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Calls",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KanalId = table.Column<long>(type: "bigint", nullable: false),
                    EntrepreneurName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LegalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VOEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermissionStartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PermissionNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDone = table.Column<bool>(type: "bit", nullable: false),
                    Conclusion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    LastStatusUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextCall = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calls", x => x.id);
                    table.ForeignKey(
                        name: "FK_Calls_Kanals_KanalId",
                        column: x => x.KanalId,
                        principalTable: "Kanals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calls_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserKanals",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    KanalId = table.Column<long>(type: "bigint", nullable: false),
                    id = table.Column<long>(type: "bigint", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKanals", x => new { x.UserId, x.KanalId });
                    table.ForeignKey(
                        name: "FK_UserKanals_Kanals_KanalId",
                        column: x => x.KanalId,
                        principalTable: "Kanals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserKanals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "CreateAt", "Email", "FullName", "Password", "Role", "isDeleted" },
                values: new object[] { 1L, new DateTime(2024, 12, 13, 15, 52, 31, 526, DateTimeKind.Local).AddTicks(8361), "admin@adra.gov.az", "Admin", "Admin123", 2, false });

            migrationBuilder.CreateIndex(
                name: "IX_Calls_KanalId",
                table: "Calls",
                column: "KanalId");

            migrationBuilder.CreateIndex(
                name: "IX_Calls_UserId",
                table: "Calls",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserKanals_KanalId",
                table: "UserKanals",
                column: "KanalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calls");

            migrationBuilder.DropTable(
                name: "UserKanals");

            migrationBuilder.DropTable(
                name: "Kanals");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
