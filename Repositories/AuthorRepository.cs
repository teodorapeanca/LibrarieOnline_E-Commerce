using LibrarieOnline.Data;
using LibrarieOnline.Models;

namespace LibrarieOnline.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}