using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingMvc.Migrations
{
    public partial class ssellerc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerDatas_AspNetUsers_SellerId",
                table: "SellerDatas");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "SellerDatas",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SellerDatas_SellerId",
                table: "SellerDatas",
                newName: "IX_SellerDatas_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerDatas_AspNetUsers_UserId",
                table: "SellerDatas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerDatas_AspNetUsers_UserId",
                table: "SellerDatas");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SellerDatas",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_SellerDatas_UserId",
                table: "SellerDatas",
                newName: "IX_SellerDatas_SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerDatas_AspNetUsers_SellerId",
                table: "SellerDatas",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
