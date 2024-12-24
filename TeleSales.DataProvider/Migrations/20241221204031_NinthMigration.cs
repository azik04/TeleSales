using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleSales.DataProvider.Migrations
{
    /// <inheritdoc />
    public partial class NinthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Departaments",
                table: "CallCenters");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "Kanals",
                newName: "Type");

            migrationBuilder.AddColumn<long>(
                name: "kanalId",
                table: "CallCenters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateAt",
                value: new DateTime(2024, 12, 22, 0, 40, 30, 996, DateTimeKind.Local).AddTicks(3449));

            migrationBuilder.CreateIndex(
                name: "IX_CallCenters_kanalId",
                table: "CallCenters",
                column: "kanalId");

            migrationBuilder.AddForeignKey(
                name: "FK_CallCenters_Kanals_kanalId",
                table: "CallCenters",
                column: "kanalId",
                principalTable: "Kanals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CallCenters_Kanals_kanalId",
                table: "CallCenters");

            migrationBuilder.DropIndex(
                name: "IX_CallCenters_kanalId",
                table: "CallCenters");

            migrationBuilder.DropColumn(
                name: "kanalId",
                table: "CallCenters");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Kanals",
                newName: "Priority");

            migrationBuilder.AddColumn<int>(
                name: "Departaments",
                table: "CallCenters",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateAt",
                value: new DateTime(2024, 12, 19, 20, 46, 4, 152, DateTimeKind.Local).AddTicks(7156));
        }
    }
}
