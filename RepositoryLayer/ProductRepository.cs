using BOs;
using BOs.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class ProductRepository : IProductRepository
    {
        private readonly EShopContext _context;
        public ProductRepository(EShopContext context)
        {
            _context = context;
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            var availableProduct = _context.Products.FirstOrDefault(a => a.ProductId == product.ProductId);
            if (availableProduct == null) return;
            _context.Products.Remove(availableProduct);
            _context.SaveChanges();
            
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();  
        }

        public Product GetProductById(int id)
        {
            var product = _context.Products.FirstOrDefault(a => a.ProductId == id);
            return product;
        }

        public List<Product> SearchProduct(string keyword, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(p => p.ProductName.Contains(keyword));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.UnitPrice >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.UnitPrice <= maxPrice.Value);
            }

            return query.ToList();
        }

        public void UpdateProduct(Product product)
        {
           var tracker = _context.Products.Attach(product);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
