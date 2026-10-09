using LibrarieOnline.Data;
using LibrarieOnline.Models;

namespace LibrarieOnline.Repositories
{
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}