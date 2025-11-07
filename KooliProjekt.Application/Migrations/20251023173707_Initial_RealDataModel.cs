using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Application.Migrations
{
    /// <inheritdoc />
    public partial class Initial_RealDataModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Tellimused");

            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "Tellimused");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Tellimused");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tellimused");

            migrationBuilder.RenameColumn(
                name: "SubTotal",
                table: "Tellimused",
                newName: "VatRate");

            migrationBuilder.RenameColumn(
                name: "ShippingTotal",
                table: "Tellimused",
                newName: "VatAmount");

            migrationBuilder.RenameColumn(
                name: "GrandTotal",
                table: "Tellimused",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Tellimused",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "VatRate",
                table: "Arved",
                newName: "SubTotal");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "Arved",
                newName: "ShippingTotal");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Arved",
                newName: "GrandTotal");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Arved",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "LineItem",
                table: "Arved",
                newName: "Status");

            migrationBuilder.AddColumn<decimal>(
                name: "LineTotal",
                table: "Tellimused",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ToodeId",
                table: "Tellimused",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Arved",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "Arved",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "Arved",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tellimused_ToodeId",
                table: "Tellimused",
                column: "ToodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tellimused_Tooted_ToodeId",
                table: "Tellimused",
                column: "ToodeId",
                principalTable: "Tooted",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tellimused_Tooted_ToodeId",
                table: "Tellimused");

            migrationBuilder.DropIndex(
                name: "IX_Tellimused_ToodeId",
                table: "Tellimused");

            migrationBuilder.DropColumn(
                name: "LineTotal",
                table: "Tellimused");

            migrationBuilder.DropColumn(
                name: "ToodeId",
                table: "Tellimused");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Arved");

            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "Arved");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Arved");

            migrationBuilder.RenameColumn(
                name: "VatRate",
                table: "Tellimused",
                newName: "SubTotal");

            migrationBuilder.RenameColumn(
                name: "VatAmount",
                table: "Tellimused",
                newName: "ShippingTotal");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "Tellimused",
                newName: "GrandTotal");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Tellimused",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "SubTotal",
                table: "Arved",
                newName: "VatRate");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Arved",
                newName: "LineItem");

            migrationBuilder.RenameColumn(
                name: "ShippingTotal",
                table: "Arved",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "GrandTotal",
                table: "Arved",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Arved",
                newName: "Quantity");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Tellimused",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "Tellimused",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "Tellimused",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Tellimused",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
