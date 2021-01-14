using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAccess.Migrations
{
    public partial class UpdateProductSizeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_productSizes_Id",
                table: "productSizes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "productSizes",
                newName: "ProductSizeId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_productSizes_ProductSizeId",
                table: "productSizes",
                column: "ProductSizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_productSizes_ProductSizeId",
                table: "productSizes");

            migrationBuilder.RenameColumn(
                name: "ProductSizeId",
                table: "productSizes",
                newName: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_productSizes_Id",
                table: "productSizes",
                column: "Id");
        }
    }
}
