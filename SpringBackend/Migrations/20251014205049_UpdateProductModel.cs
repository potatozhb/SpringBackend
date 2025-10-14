using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SKU",
                table: "products",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_products_Brand",
                table: "products",
                column: "Brand");

            migrationBuilder.CreateIndex(
                name: "IX_products_Category",
                table: "products",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_products_Name",
                table: "products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_products_SKU",
                table: "products",
                column: "SKU",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_products_Brand",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_Category",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_Name",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_SKU",
                table: "products");

            migrationBuilder.AlterColumn<string>(
                name: "SKU",
                table: "products",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }
    }
}
