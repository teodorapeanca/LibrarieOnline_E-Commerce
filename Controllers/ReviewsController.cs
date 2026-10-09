using LibrarieOnline.Data;
using LibrarieOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LibrarieOnline.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Books", new { id = review.BookId });
        }
    }
}