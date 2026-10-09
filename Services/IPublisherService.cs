using LibrarieOnline.Models;

namespace LibrarieOnline.Services
{
    public interface IPublisherService
    {
        Task<List<Publisher>> GetAllPublishers();
        Task<Publisher?> GetPublisherById(int id);
        Task AddPublisher(Publisher publisher);
        Task UpdatePublisher(Publisher publisher);
        Task DeletePublisher(int id);
        Task<bool> PublisherExists(int id);
    }
}