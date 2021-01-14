using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAccess.Migrations
{
    public partial class productSizeTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productSizes_Sizes_ProductId",
                table: "productSizes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_productSizes_ProductSizeId",
                table: "productSizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productSizes",
                table: "productSizes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productSizes",
                table: "productSizes",
                column: "ProductSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_productSizes_ProductId",
                table: "productSizes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_productSizes_SizeId",
                table: "productSizes",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_productSizes_Sizes_SizeId",
                table: "productSizes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "SizeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productSizes_Sizes_SizeId",
                table: "productSizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productSizes",
                table: "productSizes");

            migrationBuilder.DropIndex(
                name: "IX_productSizes_ProductId",
                table: "productSizes");

            migrationBuilder.DropIndex(
                name: "IX_productSizes_SizeId",
                table: "productSizes");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_productSizes_ProductSizeId",
                table: "productSizes",
                column: "ProductSizeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productSizes",
                table: "productSizes",
                columns: new[] { "ProductId", "SizeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_productSizes_Sizes_ProductId",
                table: "productSizes",
                column: "ProductId",
                principalTable: "Sizes",
                principalColumn: "SizeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
