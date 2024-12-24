using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleSales.DataProvider.Migrations
{
    /// <inheritdoc />
    public partial class SeventhMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year2024",
                table: "Calls",
                newName: "Month9_2024");

            migrationBuilder.AddColumn<decimal>(
                name: "Month10_2024",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Month11_2024",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Month12_2024",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Month1_2024",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Month1_2025",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Month2_2024",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Month2_2025",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Month3_2024",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Month3_2025",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Month4_2024",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Month5_2024",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Month6_2024",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Month7_2024",
                table: "Calls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Month8_2024",
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
                value: new DateTime(2024, 12, 18, 15, 57, 28, 742, DateTimeKind.Local).AddTicks(5243));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month10_2024",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Month11_2024",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Month12_2024",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Month1_2024",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Month1_2025",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Month2_2024",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Month2_2025",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Month3_2024",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Month3_2025",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Month4_2024",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Month5_2024",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Month6_2024",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Month7_2024",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "Month8_2024",
                table: "Calls");

            migrationBuilder.RenameColumn(
                name: "Month9_2024",
                table: "Calls",
                newName: "Year2024");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateAt",
                value: new DateTime(2024, 12, 18, 14, 47, 58, 654, DateTimeKind.Local).AddTicks(7040));
        }
    }
}
