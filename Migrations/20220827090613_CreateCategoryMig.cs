using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UdemyWEBAPI.Migrations
{
    public partial class CreateCategoryMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    categoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "CategoryName" },
                values: new object[] { 1, "Elektronik" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "CategoryName" },
                values: new object[] { 2, "Giyim" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "CreateDate", "ImagePath", "Price", "ProductName", "Stock", "categoryId" },
                values: new object[] { 1, new DateTime(2022, 8, 24, 12, 6, 13, 158, DateTimeKind.Local).AddTicks(405), null, 15000m, "Bilgisayar", 300, 1 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "CreateDate", "ImagePath", "Price", "ProductName", "Stock", "categoryId" },
                values: new object[] { 2, new DateTime(2022, 7, 28, 12, 6, 13, 158, DateTimeKind.Local).AddTicks(1633), null, 10000m, "Telefon", 500, 1 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "CreateDate", "ImagePath", "Price", "ProductName", "Stock", "categoryId" },
                values: new object[] { 3, new DateTime(2022, 6, 28, 12, 6, 13, 158, DateTimeKind.Local).AddTicks(1642), null, 100m, "Klavye", 1000, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Products_categoryId",
                table: "Products",
                column: "categoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
