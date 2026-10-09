using LibrarieOnline.Models;

namespace LibrarieOnline.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAll();
        Task<Book?> GetById(int id);
        Task Add(Book book);
        Task Update(Book book);
        Task Delete(int id);
        Task<List<Book>> SearchBooks(string searchTerm);
        Task<bool> Exists(int id);

        Task<List<Author>> GetAuthors();
        Task<List<Category>> GetCategories();
        Task<List<Publisher>> GetPublishers();

        Task AddOrderWithItem(Book book);
    }
}