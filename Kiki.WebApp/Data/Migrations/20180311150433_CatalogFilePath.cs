using Microsoft.EntityFrameworkCore.Migrations;

namespace Kiki.WebApp.Data.Migrations
{
    public partial class CatalogFilePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Catalogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Catalogs");
        }
    }
}
