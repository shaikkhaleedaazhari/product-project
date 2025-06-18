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
                    ImageUrl = "https://via.placeholder.com/300x200?text=iPhone+16e"
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Samsung Galaxy A35 5G",
                    Description = "Mid-range Samsung with 5G support",
                    Price = 150000m,
                    Stock = 40,
                    ImageUrl = "https://via.placeholder.com/300x200?text=Galaxy+A35"
                },
                new Product
                {
                    ProductId = 3,
                    Name = "OnePlus 12",
                    Description = "Flagship killer OnePlus 12",
                    Price = 64999m,
                    Stock = 60,
                    ImageUrl = "https://via.placeholder.com/300x200?text=OnePlus+12"
                }
            );
        }

    }
}
