using Microsoft.EntityFrameworkCore.Migrations;

namespace VertusNaurellesEcommerceV1.Migrations
{
    public partial class EditCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlImage",
                table: "TbCategories",
                newName: "SeoNameCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeoNameCategory",
                table: "TbCategories",
                newName: "UrlImage");
        }
    }
}
