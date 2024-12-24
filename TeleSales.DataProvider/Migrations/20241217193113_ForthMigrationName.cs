using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleSales.DataProvider.Migrations
{
    /// <inheritdoc />
    public partial class ForthMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calls_Users_UserId",
                table: "Calls");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Calls",
                newName: "ExcludedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Calls_UserId",
                table: "Calls",
                newName: "IX_Calls_ExcludedBy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateAt",
                value: new DateTime(2024, 12, 17, 23, 31, 13, 580, DateTimeKind.Local).AddTicks(9306));

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_Users_ExcludedBy",
                table: "Calls",
                column: "ExcludedBy",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calls_Users_ExcludedBy",
                table: "Calls");

            migrationBuilder.RenameColumn(
                name: "ExcludedBy",
                table: "Calls",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Calls_ExcludedBy",
                table: "Calls",
                newName: "IX_Calls_UserId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateAt",
                value: new DateTime(2024, 12, 17, 17, 1, 34, 826, DateTimeKind.Local).AddTicks(1552));

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_Users_UserId",
                table: "Calls",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
