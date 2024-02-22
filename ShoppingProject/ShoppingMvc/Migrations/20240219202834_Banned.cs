using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingMvc.Migrations
{
    public partial class Banned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isBanned",
                table: "SellerDatas",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBanned",
                table: "SellerDatas");
        }
    }
}
