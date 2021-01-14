using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAccess.Migrations
{
    public partial class identityKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productSizes_Products_ProductId",
                table: "productSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_productSizes_Sizes_SizeId",
                table: "productSizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productSizes",
                table: "productSizes");

            migrationBuilder.RenameTable(
                name: "productSizes",
                newName: "ProductSizes");

            migrationBuilder.RenameIndex(
                name: "IX_productSizes_SizeId",
                table: "ProductSizes",
                newName: "IX_ProductSizes_SizeId");

            migrationBuilder.RenameIndex(
                name: "IX_productSizes_ProductId",
                table: "ProductSizes",
                newName: "IX_ProductSizes_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSizes",
                table: "ProductSizes",
                column: "ProductSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_Products_ProductId",
                table: "ProductSizes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_Sizes_SizeId",
                table: "ProductSizes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "SizeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_Products_ProductId",
                table: "ProductSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_Sizes_SizeId",
                table: "ProductSizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSizes",
                table: "ProductSizes");

            migrationBuilder.RenameTable(
                name: "ProductSizes",
                newName: "productSizes");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizes_SizeId",
                table: "productSizes",
                newName: "IX_productSizes_SizeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizes_ProductId",
                table: "productSizes",
                newName: "IX_productSizes_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productSizes",
                table: "productSizes",
                column: "ProductSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_productSizes_Products_ProductId",
                table: "productSizes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productSizes_Sizes_SizeId",
                table: "productSizes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "SizeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
