using LibrarieOnline.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarieOnline.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IBookService _bookService;

        public CatalogController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooks();

            return View(books);
        }

        public IActionResult Detalii()
        {
            return View();
        }
    }
}