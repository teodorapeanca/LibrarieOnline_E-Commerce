using LibrarieOnline.Data;
using LibrarieOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarieOnline.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new async Task<List<Book>> GetAll()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .ToListAsync();
        }

        public new async Task<Book?> GetById(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(b => b.BookId == id);
        }

        public async Task<List<Author>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Publisher>> GetPublishers()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task AddOrderWithItem(Book book)
        {
            var order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = book.Price
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderItem = new OrderItem
            {
                OrderId = order.OrderId,
                BookId = book.BookId,
                Quantity = 1,
                UnitPrice = book.Price
            };

            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Book>> SearchBooks(string searchTerm)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .Where(b => b.Title.Contains(searchTerm)
                         || b.Author.Name.Contains(searchTerm)
                         || b.Category.Name.Contains(searchTerm)
                         || b.Publisher.Name.Contains(searchTerm))
                .ToListAsync();
        }
    }
}