using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleSales.DataProvider.Migrations
{
    /// <inheritdoc />
    public partial class EightMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallCenters",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExcludedBy = table.Column<long>(type: "bigint", nullable: false),
                    VOEN = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationType = table.Column<int>(type: "int", nullable: false),
                    ShortContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetailsContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Forwarding = table.Column<bool>(type: "bit", nullable: false),
                    Administration = table.Column<int>(type: "int", nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    ForwardTo = table.Column<long>(type: "bigint", nullable: true),
                    Conclusion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Addition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Departaments = table.Column<int>(type: "int", nullable: true),
                    Usersid = table.Column<long>(type: "bigint", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallCenters", x => x.id);
                    table.ForeignKey(
                        name: "FK_CallCenters_Users_Usersid",
                        column: x => x.Usersid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateAt",
                value: new DateTime(2024, 12, 19, 20, 46, 4, 152, DateTimeKind.Local).AddTicks(7156));

            migrationBuilder.CreateIndex(
                name: "IX_CallCenters_Usersid",
                table: "CallCenters",
                column: "Usersid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallCenters");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateAt",
                value: new DateTime(2024, 12, 18, 15, 57, 28, 742, DateTimeKind.Local).AddTicks(5243));
        }
    }
}
