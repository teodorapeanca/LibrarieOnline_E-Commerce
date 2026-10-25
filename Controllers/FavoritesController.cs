
using System.Security.Claims;
using LibrarieOnline.Data;
using LibrarieOnline.Models;
using Microsoft.AspNetCore.Authorization;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favorites = await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Book)
                    .ThenInclude(b => b.Author)
                .Include(f => f.Book.Category)
                .Include(f => f.Book.Publisher)
                .ToListAsync();

            return View(favorites);
        }

        public async Task<IActionResult> Add(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
                return Challenge();

            var bookExists = await _context.Books
                .AnyAsync(b => b.BookId == id);

            if (!bookExists)
                return NotFound();

            var exists = await _context.Favorites
                .AnyAsync(f => f.BookId == id && f.UserId == userId);

            if (!exists)
            {
                var favorite = new Favorite
                {
                    BookId = id,
                    UserId = userId
                };

                _context.Favorites.Add(favorite);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
                return Challenge();

            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f =>
                    f.FavoriteId == id && f.UserId == userId);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
