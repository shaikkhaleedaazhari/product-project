using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductService.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "Latest Apple iPhone with 128 GB storage", "https://s3.ap-south-1.amazonaws.com/shop.unicorn/full/51f2fa0c3fbd7d32e4c78dd85.webp", "iPhone 16e 128 GB", 56000m, 50 },
                    { 2, "Mid-range Samsung with 5G support", "https://m.media-amazon.com/images/I/41N3oDyg3FL._SX300_SY300_QL70_FMwebp_.jpg", "Samsung Galaxy A35 5G", 150000m, 40 },
                    { 3, "Flagship killer OnePlus 12", "https://m.media-amazon.com/images/I/41J4+TiUz6L._SY300_SX300_.jpg", "OnePlus 12", 64999m, 60 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
