using LibrarieOnline.Models;

namespace LibrarieOnline.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();

        Task<Category?> GetCategoryById(int id);

        Task AddCategory(Category category);

        Task UpdateCategory(Category category);

        Task DeleteCategory(int id);

        Task<bool> CategoryExists(int id);
    }
}