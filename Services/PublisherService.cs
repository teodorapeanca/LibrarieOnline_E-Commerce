using LibrarieOnline.Models;
using LibrarieOnline.Repositories;

namespace LibrarieOnline.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task<List<Publisher>> GetAllPublishers()
        {
            return await _publisherRepository.GetAll();
        }

        public async Task<Publisher?> GetPublisherById(int id)
        {
            return await _publisherRepository.GetById(id);
        }

        public async Task AddPublisher(Publisher publisher)
        {
            await _publisherRepository.Add(publisher);
        }

        public async Task UpdatePublisher(Publisher publisher)
        {
            await _publisherRepository.Update(publisher);
        }

        public async Task DeletePublisher(int id)
        {
            await _publisherRepository.Delete(id);
        }

        public async Task<bool> PublisherExists(int id)
        {
            return await _publisherRepository.Exists(id);
        }
    }
}