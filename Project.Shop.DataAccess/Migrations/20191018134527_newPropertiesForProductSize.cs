using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAccess.Migrations
{
    public partial class newPropertiesForProductSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinimumReorderLevel",
                table: "productSizes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockLevel",
                table: "productSizes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumReorderLevel",
                table: "productSizes");

            migrationBuilder.DropColumn(
                name: "StockLevel",
                table: "productSizes");
        }
    }
}
