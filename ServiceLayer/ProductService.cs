using BOs.Entities;
using RepositoryLayer;
using RepositoryLayer.Interfaces;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ProductService : IProductService
    {
		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public void CreateProduct(Product product)
        {
            _productRepository.CreateProduct(product);
        }

        public void DeleteProduct(Product product)
        {
            _productRepository.DeleteProduct(product);
        }

        public List<Product> GetAllProducts()
        {
           return _productRepository.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public List<Product> SearchProduct(string keyword, decimal? minPrice, decimal? maxPrice)
        {
            return _productRepository.SearchProduct(keyword, minPrice, maxPrice);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);
        }
    }
}
