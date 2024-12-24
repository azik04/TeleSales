using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleSales.DataProvider.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntrepreneurName",
                table: "Calls",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Calls",
                newName: "District");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Calls",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PermissionStartDate",
                table: "Calls",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Conclusion",
                table: "Calls",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "PermissionEndDate",
                table: "Calls",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDebt",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Year2018",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Year2019",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Year2020",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Year2021",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Year2022",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Year2023",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Year2024",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateAt",
                value: new DateTime(2024, 12, 17, 15, 24, 28, 91, DateTimeKind.Local).AddTicks(8492));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionEndDate",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "TotalDebt",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Year2018",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Year2019",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Year2020",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Year2021",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Year2022",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Year2023",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Year2024",
                table: "Calls");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Calls",
                newName: "EntrepreneurName");

            migrationBuilder.RenameColumn(
                name: "District",
                table: "Calls",
                newName: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Calls",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PermissionStartDate",
                table: "Calls",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Conclusion",
                table: "Calls",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateAt",
                value: new DateTime(2024, 12, 13, 15, 52, 31, 526, DateTimeKind.Local).AddTicks(8361));
        }
    }
}
