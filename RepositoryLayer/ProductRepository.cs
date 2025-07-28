using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.Entities;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer
{
    public class ProductRepository
    {
        private readonly EShopContext _context;
        public ProductRepository(EShopContext context)
        {
            _context = context;
        }
        // Thêm các phương thức truy xuất dữ liệu sản phẩm tại đây
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p=>p.Category).ToListAsync();
        }
        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _context.Products.
                Include(p => p.Category).
                FirstOrDefaultAsync(p=>p.ProductId==productId);
        }
        public async Task<List<Product>> GetProductsAsync(string search = null, int? categoryId = null)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.ProductName.Contains(search));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            return await query.ToListAsync();
        }
        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(int productId)
        {
            var product = await GetProductByIdAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
