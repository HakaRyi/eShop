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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EShopContext _context;
        public CategoryRepository(EShopContext context)
        {
            _context = context;
        }

        public void CreateCategory(Category category)
        {
           _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            var availableCategory = _context.Categories.FirstOrDefault(a => a.CategoryId == category.CategoryId);
            if (availableCategory == null) return;
            _context.Categories.Remove(availableCategory);
            _context.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(a => a.CategoryId == id);
            return category;
        }

        public void UpdateCategory(Category category)
        {
            var tracker = _context.Categories.Attach(category);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
