using Microsoft.EntityFrameworkCore.Migrations;

namespace VertusNaurellesEcommerceV1.Migrations
{
    public partial class ChangeRelationCategoryAndPrecisionDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbProducts_TbCategories_products",
                table: "TbProducts");

            migrationBuilder.DropIndex(
                name: "IX_TbProducts_products",
                table: "TbProducts");

            migrationBuilder.DropColumn(
                name: "products",
                table: "TbProducts");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalesPrice",
                table: "TbProducts",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PurchasePrice",
                table: "TbProducts",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "OrderTotal",
                table: "TbOrders",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "TbOrderItems",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SalesPrice",
                table: "TbProducts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PurchasePrice",
                table: "TbProducts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AddColumn<int>(
                name: "products",
                table: "TbProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OrderTotal",
                table: "TbOrders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "TbOrderItems",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.CreateIndex(
                name: "IX_TbProducts_products",
                table: "TbProducts",
                column: "products");

            migrationBuilder.AddForeignKey(
                name: "FK_TbProducts_TbCategories_products",
                table: "TbProducts",
                column: "products",
                principalTable: "TbCategories",
                principalColumn: "IdCategory",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
