
using System.Security.Claims;
using LibrarieOnline.Data;
using LibrarieOnline.Models;
using LibrarieOnline.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarieOnline.Controllers
{
    [Authorize(Roles = "User")]
    public class CosController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ApplicationDbContext _context;

        public CosController(
            IBookService bookService,
            ApplicationDbContext context)
        {
            _bookService = bookService;
            _context = context;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new InvalidOperationException(
                    "Utilizatorul nu este autentificat.");
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();

            var cart = await _context.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();

            var cartItems = new List<CartItemViewModel>();

            foreach (var item in cart)
            {
                var book = await _bookService.GetBookById(item.BookId);

                if (book != null)
                {
                    cartItems.Add(new CartItemViewModel
                    {
                        Book = book,
                        Quantity = item.Quantity
                    });
                }
            }

            return View(cartItems);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var userId = GetUserId();

            var book = await _bookService.GetBookById(id);

            if (book == null)
                return NotFound();

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c =>
                    c.UserId == userId && c.BookId == id);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    UserId = userId,
                    BookId = id,
                    Quantity = 1
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var userId = GetUserId();

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c =>
                    c.UserId == userId && c.BookId == id);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Clear()
        {
            var userId = GetUserId();

            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();

            _context.CartItems.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> FinalizeazaComanda()
        {
            var userId = GetUserId();

            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
            {
                TempData["Message"] = "Coșul este gol.";
                return RedirectToAction(nameof(Index));
            }

            // Deocamdata pastram comportamentul existent:
            // confirmam finalizarea si golim cosul utilizatorului.
            _context.CartItems.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            TempData["Message"] =
                "Comanda a fost finalizată cu succes!";

            return RedirectToAction(nameof(Index));
        }
    }
}
