using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantsApp.Migrations
{
    public partial class UpdatedRestaurantProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Restaurants",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Restaurants");
        }
    }
}
