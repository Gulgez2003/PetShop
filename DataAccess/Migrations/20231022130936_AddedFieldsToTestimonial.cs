using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddedFieldsToTestimonial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Testimonials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Testimonials_ProductId",
                table: "Testimonials",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Testimonials_Products_ProductId",
                table: "Testimonials",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Testimonials_Products_ProductId",
                table: "Testimonials");

            migrationBuilder.DropIndex(
                name: "IX_Testimonials_ProductId",
                table: "Testimonials");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Testimonials");
        }
    }
}
