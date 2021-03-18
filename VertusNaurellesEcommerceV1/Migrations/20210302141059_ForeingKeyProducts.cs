using Microsoft.EntityFrameworkCore.Migrations;

namespace VertusNaurellesEcommerceV1.Migrations
{
    public partial class ForeingKeyProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UrlImage",
                table: "TbSliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LongDescription",
                table: "TbProducts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "products",
                table: "TbProducts",
                type: "int",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UrlImage",
                table: "TbSliders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LongDescription",
                table: "TbProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
