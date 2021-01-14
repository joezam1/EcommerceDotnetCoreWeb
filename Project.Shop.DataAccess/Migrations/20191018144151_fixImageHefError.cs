using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAccess.Migrations
{
    public partial class fixImageHefError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageHref15",
                table: "Products",
                newName: "ImageHref5");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageHref5",
                table: "Products",
                newName: "ImageHref15");
        }
    }
}
