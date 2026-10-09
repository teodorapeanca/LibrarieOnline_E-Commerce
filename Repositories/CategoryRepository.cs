using LibrarieOnline.Data;
using LibrarieOnline.Models;

namespace LibrarieOnline.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}