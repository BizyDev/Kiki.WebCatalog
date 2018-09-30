using Microsoft.EntityFrameworkCore.Migrations;

namespace Kiki.WebApp.Migrations
{
    public partial class GaragesPrices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FinalPriceGarage",
                table: "Products",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MarginGarage",
                table: "DiscountRules",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalPriceGarage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MarginGarage",
                table: "DiscountRules");
        }
    }
}
