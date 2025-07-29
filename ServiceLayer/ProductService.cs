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
            var products = await productRepository.GetAllProductsAsync(search, categoryId);
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
        public async Task<ProductResponse> GetPagedProductsAsync(string search = null, int? categoryId = null, int pageIndex = 1, int pageSize = 9)
        {
            var (products, totalCount) = await productRepository.GetPagedProductsAsync(search, categoryId, pageIndex, pageSize);

            var productDtos = products.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice,
                Weight = p.Weight,
                UnitsInStock = p.UnitsInStock,
                CategoryName = p.Category?.CategoryName ?? "N/A"
            }).ToList();

            return new ProductResponse
            {
                Products = productDtos,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                PageIndex = pageIndex
            };
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