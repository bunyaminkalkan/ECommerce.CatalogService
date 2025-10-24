using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.CatalogService.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_products_InventoryItemId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_IsActive_Price",
                table: "products");

            migrationBuilder.DropColumn(
                name: "InventoryItemId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "products",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "products",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "products");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "products");

            migrationBuilder.AddColumn<Guid>(
                name: "InventoryItemId",
                table: "products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "products",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_products_InventoryItemId",
                table: "products",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_products_IsActive_Price",
                table: "products",
                columns: new[] { "IsActive", "Price" });
        }
    }
}
