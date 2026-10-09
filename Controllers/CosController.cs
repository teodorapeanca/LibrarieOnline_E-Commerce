using LibrarieOnline.Models;
using LibrarieOnline.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibrarieOnline.Controllers
{
    [Authorize(Roles = "User")]
    public class CosController : Controller
    {
        private readonly IBookService _bookService;

        public CosController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var cart = GetCart();

            var cartItems = new List<CartItemViewModel>();

            foreach (var group in cart.GroupBy(id => id))
            {
                var book = await _bookService.GetBookById(group.Key);

                if (book != null)
                {
                    cartItems.Add(new CartItemViewModel
                    {
                        Book = book,
                        Quantity = group.Count()
                    });
                }
            }

            return View(cartItems);
        }

        public IActionResult AddToCart(int id)
        {
            var cart = GetCart();

            cart.Add(id);

            SaveCart(cart);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = GetCart();

            cart.Remove(id);

            SaveCart(cart);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult FinalizeazaComanda()
        {
            HttpContext.Session.Remove("Cart");

            TempData["Message"] = "Comanda a fost finalizată cu succes!";

            return RedirectToAction(nameof(Index));
        }

        private List<int> GetCart()
        {
            var cart = HttpContext.Session.GetString("Cart");

            return string.IsNullOrEmpty(cart)
                ? new List<int>()
                : JsonSerializer.Deserialize<List<int>>(cart) ?? new List<int>();
        }

        private void SaveCart(List<int> cart)
        {
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
        }
    }
}