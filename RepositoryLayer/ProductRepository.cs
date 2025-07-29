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
        //public async Task<List<Product>> GetAllProductsAsync()
        //{
        //    return await _context.Products.Include(p=>p.Category).ToListAsync();
        //}
        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _context.Products.
                Include(p => p.Category).
                FirstOrDefaultAsync(p => p.ProductId == productId);
        }
        public async Task<List<Product>> GetAllProductsAsync(string search = null, int? categoryId = null)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.ProductName.ToLower().Contains(search.Trim().ToLower()));
            }


            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }


            if (string.IsNullOrWhiteSpace(search) && !categoryId.HasValue)
            {
                query = query.OrderByDescending(p => p.ProductId);
            }

            return await query.ToListAsync();
        }
        public async Task<(List<Product> Products, int TotalCount)> GetPagedProductsAsync(
    string search = null, int? categoryId = null, int pageIndex = 1, int pageSize = 9)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var trimmedSearch = search.Trim().ToLower();
                if (decimal.TryParse(trimmedSearch, out decimal parsedPrice))
                {
                    query = query.Where(p =>
                        p.ProductName.ToLower().Contains(trimmedSearch)
                        || p.UnitPrice == parsedPrice
                    );
                }
                else
                {
                    query = query.Where(p =>
                        p.ProductName.ToLower().Contains(trimmedSearch)
                    );
                }
            }
            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            int totalCount = await query.CountAsync();

            var products = await query
                .OrderByDescending(p => p.ProductId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
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