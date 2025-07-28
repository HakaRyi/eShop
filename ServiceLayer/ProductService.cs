using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer;
using ServiceLayer.DTO;

namespace ServiceLayer
{
    public class ProductService
    {
        private readonly ProductRepository productRepository;
        public ProductService(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<List<ProductDTO>> GetAllProductsAsync(string search = null, int? categoryId = null)
        {
            var products = await productRepository.GetProductsAsync(search, categoryId);
            return products.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice,
                Weight = p.Weight,
                UnitsInStock = p.UnitsInStock,
                CategoryName = p.Category?.CategoryName ?? "N/A"
            }).ToList();
        }
        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await productRepository.GetProductByIdAsync(id);
            if (product == null) return null;
            return new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                Weight = product.Weight,
                UnitsInStock = product.UnitsInStock,
                CategoryName = product.Category?.CategoryName ?? "N/A"
            };
        }
        public async Task<List<CategoryDTO>> GetCategoriesAsync()
        {
            var categories = await productRepository.GetCategoriesAsync();
            return categories.Select(c => new CategoryDTO
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }).ToList();
        }
    }
}
