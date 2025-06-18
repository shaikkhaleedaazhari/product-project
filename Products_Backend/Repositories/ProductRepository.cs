using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductService.Repositories
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) => _context = context;

        public Task<List<Product>> GetAllAsync() => _context.Products.ToListAsync();
        public Task<Product?> GetByIdAsync(int id) => _context.Products.FindAsync(id).AsTask();

        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}