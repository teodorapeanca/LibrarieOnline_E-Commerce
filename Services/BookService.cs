using LibrarieOnline.Models;
using LibrarieOnline.Repositories;

namespace LibrarieOnline.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _bookRepository.GetAll();
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await _bookRepository.GetById(id);
        }

        public async Task AddBook(Book book)
        {
            await _bookRepository.Add(book);
        }

        public async Task UpdateBook(Book book)
        {
            await _bookRepository.Update(book);
        }

        public async Task DeleteBook(int id)
        {
            await _bookRepository.Delete(id);
        }

        public async Task<bool> BookExists(int id)
        {
            return await _bookRepository.Exists(id);
        }

        public async Task<List<Author>> GetAuthors()
        {
            return await _bookRepository.GetAuthors();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _bookRepository.GetCategories();
        }

        public async Task<List<Publisher>> GetPublishers()
        {
            return await _bookRepository.GetPublishers();
        }

        public async Task AddToCart(int id)
        {
            var book = await _bookRepository.GetById(id);

            if (book != null)
            {
                await _bookRepository.AddOrderWithItem(book);
            }
        }

        public async Task<List<Book>> SearchBooks(string searchTerm)
        {
            return await _bookRepository.SearchBooks(searchTerm);
        }
    }
}