using BusinessObjects;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;

namespace Repositories.Implement
{
    public class CategoryRepo : ICategoryRepo
    {
        public async Task<List<Category>> GetCategoriesAsync()
        {
            using (FunewsManagementDbContext _context = new())
            {
                return await _context.Categories.Include(x => x.NewsArticles).ToListAsync();
            }
        }

        public async Task<Category> GetCategoryByIdAsync(short id)
        {
            using (FunewsManagementDbContext _context = new())
            {
                return await _context.Categories.FindAsync(id);
            }
        }

        public async Task AddCategoryAsync(Category category)
        {
            using (FunewsManagementDbContext _context = new())
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveCategoryAsync(short id)
        {
            using (FunewsManagementDbContext _context = new())
            {
                var categoryToRemove = await _context.Categories.FindAsync(id);

                if (categoryToRemove != null)
                {
                    try
                    {
                        _context.Categories.Remove(categoryToRemove);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Cannot delete this category", ex);
                    }
                }
                else
                {
                    throw new ArgumentException("Category not found", nameof(id));
                }
            }
        }

        public async Task UpdateCategoryAsync(Category newCategory)
        {
            using (FunewsManagementDbContext _context = new())
            {
                var existingCategory = await GetCategoryByIdAsync(newCategory.CategoryId);

                if (existingCategory != null)
                {
                    existingCategory.CategoryName = newCategory.CategoryName;
                    existingCategory.CategoryDesciption = newCategory.CategoryDesciption;

                    _context.Categories.Update(existingCategory);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Category not found", nameof(newCategory));
                }
            }
        }

        public async Task<List<Category>> SearchCategoriesByNameAsync(string categoryName)
        {
            using (FunewsManagementDbContext _context = new())
            {
                return await _context.Categories
                                     .Where(c => c.CategoryName.Contains(categoryName))
                                     .ToListAsync();
            }
        }
    }
}
