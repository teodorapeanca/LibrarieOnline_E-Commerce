using LibrarieOnline.Models;

namespace LibrarieOnline.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();

        Task<Book?> GetBookById(int id);

        Task AddBook(Book book);

        Task UpdateBook(Book book);

        Task DeleteBook(int id);

        Task<bool> BookExists(int id);
        Task<List<Book>> SearchBooks(string searchTerm);

        Task<List<Author>> GetAuthors();

        Task<List<Category>> GetCategories();

        Task<List<Publisher>> GetPublishers();

        Task AddToCart(int id);
    }
}