using Microsoft.EntityFrameworkCore;

using ProductService.Models;
 
namespace ProductService.Data

{

    public class ApplicationDbContext : DbContext

    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)

            : base(options) { }
 
        public DbSet<Product> Products { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            base.OnModelCreating(modelBuilder);
 
            modelBuilder.Entity<Product>().HasData(

                new Product

                {

                    ProductId = 1,

                    Name = "iPhone 16e 128 GB",

                    Description = "Latest Apple iPhone with 128 GB storage",

                    Price = 56000m,

                    Stock = 50,

                    ImageUrl = "https://s3.ap-south-1.amazonaws.com/shop.unicorn/full/51f2fa0c3fbd7d32e4c78dd85.webp"

                },

                new Product

                {

                    ProductId = 2,

                    Name = "Samsung Galaxy A35 5G",

                    Description = "Mid-range Samsung with 5G support",

                    Price = 150000m,

                    Stock = 40,

                    ImageUrl = "https://m.media-amazon.com/images/I/41N3oDyg3FL._SX300_SY300_QL70_FMwebp_.jpg"

                },

                new Product

                {

                    ProductId = 3,

                    Name = "OnePlus 12",

                    Description = "Flagship killer OnePlus 12",

                    Price = 64999m,

                    Stock = 60,

                    ImageUrl = "https://m.media-amazon.com/images/I/41J4+TiUz6L._SY300_SX300_.jpg"

                }

            );

        }
 
    }

}

 
