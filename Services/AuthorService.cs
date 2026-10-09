using LibrarieOnline.Models;
using LibrarieOnline.Repositories;

namespace LibrarieOnline.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _authorRepository.GetAll();
        }

        public async Task<Author?> GetAuthorById(int id)
        {
            return await _authorRepository.GetById(id);
        }

        public async Task AddAuthor(Author author)
        {
            await _authorRepository.Add(author);
        }

        public async Task UpdateAuthor(Author author)
        {
            await _authorRepository.Update(author);
        }

        public async Task DeleteAuthor(int id)
        {
            await _authorRepository.Delete(id);
        }

        public async Task<bool> AuthorExists(int id)
        {
            return await _authorRepository.Exists(id);
        }
    }
}