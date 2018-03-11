using Microsoft.EntityFrameworkCore.Migrations;

namespace Kiki.WebApp.Data.Migrations
{
    public partial class CatalogRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeColumnRegex",
                table: "Catalogs");

            migrationBuilder.AddColumn<int>(
                name: "SheetIndex",
                table: "Catalogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeFormat",
                table: "Catalogs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SheetIndex",
                table: "Catalogs");

            migrationBuilder.DropColumn(
                name: "SizeFormat",
                table: "Catalogs");

            migrationBuilder.AddColumn<string>(
                name: "SizeColumnRegex",
                table: "Catalogs",
                nullable: true);
        }
    }
}
