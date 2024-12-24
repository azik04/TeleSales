using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleSales.DataProvider.Migrations
{
    /// <inheritdoc />
    public partial class ThierdMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "PermissionStartDate",
                table: "Calls",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PermissionEndDate",
                table: "Calls",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateAt",
                value: new DateTime(2024, 12, 17, 17, 1, 34, 826, DateTimeKind.Local).AddTicks(1552));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "PermissionStartDate",
                table: "Calls",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PermissionEndDate",
                table: "Calls",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateAt",
                value: new DateTime(2024, 12, 17, 15, 24, 28, 91, DateTimeKind.Local).AddTicks(8492));
        }
    }
}
