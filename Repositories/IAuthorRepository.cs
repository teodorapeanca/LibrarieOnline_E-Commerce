
using LibrarieOnline.Models;

namespace LibrarieOnline.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAll();
        Task<Author?> GetById(int id);

        // Obține autorul împreună cu toate cărțile sale
        Task<Author?> GetAuthorWithBooks(int id);

        Task Add(Author author);
        Task Update(Author author);
        Task Delete(int id);
        Task<bool> Exists(int id);
    }
}
