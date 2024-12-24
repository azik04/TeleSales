using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleSales.DataProvider.Migrations
{
    /// <inheritdoc />
    public partial class FifthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PermissionNumber",
                table: "Calls",
                newName: "Subject");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "Calls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateAt",
                value: new DateTime(2024, 12, 18, 14, 42, 39, 153, DateTimeKind.Local).AddTicks(2868));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Calls");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Calls",
                newName: "PermissionNumber");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateAt",
                value: new DateTime(2024, 12, 17, 23, 31, 13, 580, DateTimeKind.Local).AddTicks(9306));
        }
    }
}
