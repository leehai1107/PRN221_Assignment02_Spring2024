using BusinessObjects;

namespace Services.Interface
{
    public interface ICategorySvc
    {
        Task<List<Category>> GetCategoriesAsync();
        Task AddCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(short id);
        Task RemoveCategoryAsync(short id);
        Task UpdateCategoryAsync(Category newCategory);
        Task<List<Category>> SearchCategoriesByNameAsync(string categoryName);
    }
}
