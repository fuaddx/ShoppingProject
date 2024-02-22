using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingMvc.Migrations
{
    public partial class IsBanned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isBanned",
                table: "Tags",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isBanned",
                table: "Sliders",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isBanned",
                table: "Replys",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isBanned",
                table: "Products",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isBanned",
                table: "ProductImages",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isBanned",
                table: "Comments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isBanned",
                table: "Categories",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isBanned",
                table: "Baskets",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isBanned",
                table: "BasketItems",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isBanned",
                table: "AdditionalInfos",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBanned",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "isBanned",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "isBanned",
                table: "Replys");

            migrationBuilder.DropColumn(
                name: "isBanned",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "isBanned",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "isBanned",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "isBanned",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "isBanned",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "isBanned",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "isBanned",
                table: "AdditionalInfos");
        }
    }
}
