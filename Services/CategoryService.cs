using LibrarieOnline.Models;
using LibrarieOnline.Repositories;

namespace LibrarieOnline.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _categoryRepository.GetById(id);
        }

        public async Task AddCategory(Category category)
        {
            await _categoryRepository.Add(category);
        }

        public async Task UpdateCategory(Category category)
        {
            await _categoryRepository.Update(category);
        }

        public async Task DeleteCategory(int id)
        {
            await _categoryRepository.Delete(id);
        }

        public async Task<bool> CategoryExists(int id)
        {
            return await _categoryRepository.Exists(id);
        }
    }
}