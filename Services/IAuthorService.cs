using LibrarieOnline.Models;

namespace LibrarieOnline.Services
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAllAuthors();

        Task<Author?> GetAuthorById(int id);

        Task AddAuthor(Author author);

        Task UpdateAuthor(Author author);

        Task DeleteAuthor(int id);

        Task<bool> AuthorExists(int id);
    }
}