using BusinessObjects;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implement
{
    public class CategorySvc : ICategorySvc
    {
        private readonly ICategoryRepo _categoryRepo;
        public CategorySvc(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _categoryRepo.GetCategoriesAsync();
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _categoryRepo.AddCategoryAsync(category);
        }

        public async Task<Category> GetCategoryByIdAsync(short id)
        {
            return await _categoryRepo.GetCategoryByIdAsync(id);
        }

        public async Task RemoveCategoryAsync(short id)
        {
            await _categoryRepo.RemoveCategoryAsync(id);
        }

        public async Task UpdateCategoryAsync(Category newCategory)
        {
            await _categoryRepo.UpdateCategoryAsync(newCategory);
        }

        public async Task<List<Category>> SearchCategoriesByNameAsync(string categoryName)
        {
            return await _categoryRepo.SearchCategoriesByNameAsync(categoryName);
        }
    }
}
