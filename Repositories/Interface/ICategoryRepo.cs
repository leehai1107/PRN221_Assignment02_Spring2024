using BusinessObjects;

namespace Repositories.Interface
{
    public interface ICategoryRepo
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(short id);
        Task RemoveCategoryAsync(short id);
        Task UpdateCategoryAsync(Category newCategory);
        Task AddCategoryAsync(Category category);
        Task<List<Category>> SearchCategoriesByNameAsync(string categoryName);
    }
}
