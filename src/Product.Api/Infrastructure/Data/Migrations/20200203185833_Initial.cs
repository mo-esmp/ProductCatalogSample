using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Catalog.Api.Infrastructure.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 64, nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 64, nullable: true),
                    Price_Amount = table.Column<decimal>(nullable: true),
                    Price_Currency_CurrencyCode = table.Column<int>(nullable: true),
                    Price_Currency_InUse = table.Column<bool>(nullable: true),
                    Price_Currency_DecimalPlaces = table.Column<int>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}