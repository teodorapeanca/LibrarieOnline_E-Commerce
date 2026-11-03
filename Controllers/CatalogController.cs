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

        public async Task<IActionResult> Index(int? id)
        {
            var books = await _bookService.GetAllBooks();

            ViewBag.SelectedBookId = id;

            return View(books);
        }


        public async Task<IActionResult> Detalii(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBookById(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

    }
}