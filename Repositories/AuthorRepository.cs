
using LibrarieOnline.Data;
using LibrarieOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarieOnline.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Author?> GetAuthorWithBooks(int id)
        {
            return await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.AuthorId == id);
        }
    }
}
