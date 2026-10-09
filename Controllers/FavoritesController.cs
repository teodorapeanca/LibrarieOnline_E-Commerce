using Microsoft.AspNetCore.Authorization;
using LibrarieOnline.Data;
using LibrarieOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarieOnline.Controllers
{
    [Authorize(Roles = "User")]
    public class FavoritesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoritesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var favorites = await _context.Favorites
                .Include(f => f.Book)
                .ThenInclude(b => b.Author)
                .Include(f => f.Book.Category)
                .Include(f => f.Book.Publisher)
                .ToListAsync();

            return View(favorites);
        }

        public async Task<IActionResult> Add(int id)
        {
            var exists = await _context.Favorites
                .AnyAsync(f => f.BookId == id);

            if (!exists)
            {
                var favorite = new Favorite
                {
                    BookId = id
                };

                _context.Favorites.Add(favorite);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Books");
        }

        public async Task<IActionResult> Remove(int id)
        {
            var favorite = await _context.Favorites.FindAsync(id);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}