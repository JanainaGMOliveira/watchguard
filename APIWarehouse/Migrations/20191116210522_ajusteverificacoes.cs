using Microsoft.EntityFrameworkCore.Migrations;

namespace APIWarehouse.Migrations
{
    public partial class ajusteverificacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_product_tb_brand_BrandId",
                table: "tb_product");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_product_tb_brand_BrandId",
                table: "tb_product",
                column: "BrandId",
                principalTable: "tb_brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_product_tb_brand_BrandId",
                table: "tb_product");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_product_tb_brand_BrandId",
                table: "tb_product",
                column: "BrandId",
                principalTable: "tb_brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
