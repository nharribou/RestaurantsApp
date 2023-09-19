using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantsApp.Migrations
{
    public partial class UpdatedRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_CategoryId",
                table: "Restaurants",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Categories_CategoryId",
                table: "Restaurants",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Categories_CategoryId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_CategoryId",
                table: "Restaurants");
        }
    }
}
