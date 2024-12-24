using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleSales.DataProvider.Migrations
{
    /// <inheritdoc />
    public partial class PreProd2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CallCenters_Users_Usersid",
                table: "CallCenters");

            migrationBuilder.DropIndex(
                name: "IX_CallCenters_Usersid",
                table: "CallCenters");

            migrationBuilder.DropColumn(
                name: "Usersid",
                table: "CallCenters");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateAt",
                value: new DateTime(2024, 12, 22, 2, 57, 41, 516, DateTimeKind.Local).AddTicks(4925));

            migrationBuilder.CreateIndex(
                name: "IX_CallCenters_ExcludedBy",
                table: "CallCenters",
                column: "ExcludedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_CallCenters_Users_ExcludedBy",
                table: "CallCenters",
                column: "ExcludedBy",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CallCenters_Users_ExcludedBy",
                table: "CallCenters");

            migrationBuilder.DropIndex(
                name: "IX_CallCenters_ExcludedBy",
                table: "CallCenters");

            migrationBuilder.AddColumn<long>(
                name: "Usersid",
                table: "CallCenters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateAt",
                value: new DateTime(2024, 12, 22, 2, 31, 18, 474, DateTimeKind.Local).AddTicks(2699));

            migrationBuilder.CreateIndex(
                name: "IX_CallCenters_Usersid",
                table: "CallCenters",
                column: "Usersid");

            migrationBuilder.AddForeignKey(
                name: "FK_CallCenters_Users_Usersid",
                table: "CallCenters",
                column: "Usersid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
