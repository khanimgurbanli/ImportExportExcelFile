using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceAPI.Persistence.Migrations
{
    public partial class Createdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Segment = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    Product = table.Column<string>(type: "TEXT", nullable: false),
                    DiscountBand = table.Column<string>(type: "TEXT", nullable: false),
                    UnitsSold = table.Column<double>(type: "REAL", nullable: false),
                    ManufactoringSold = table.Column<double>(type: "REAL", nullable: false),
                    SalePrice = table.Column<double>(type: "REAL", nullable: false),
                    GrossSales = table.Column<double>(type: "REAL", nullable: false),
                    Discounts = table.Column<double>(type: "REAL", nullable: false),
                    Sales = table.Column<double>(type: "REAL", nullable: false),
                    COGS = table.Column<double>(type: "REAL", nullable: false),
                    Profit = table.Column<double>(type: "REAL", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");
        }
    }
}
