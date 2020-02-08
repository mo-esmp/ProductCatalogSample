using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.Api.Infrastructure.Data.Migrations
{
    public partial class AddPhotoNameToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoName",
                table: "Products",
                unicode: false,
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoName",
                table: "Products");
        }
    }
}
