using LibrarieOnline.Models;
using LibrarieOnline.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibrarieOnline.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var searchedBooks = await _bookService.SearchBooks(searchTerm);
                return View(searchedBooks);
            }

            var books = await _bookService.GetAllBooks();
            return View(books);
        }

        public async Task<IActionResult> Details(int? id)
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = new SelectList(await _bookService.GetAuthors(), "AuthorId", "Name");
            ViewBag.Categories = new SelectList(await _bookService.GetCategories(), "CategoryId", "Name");
            ViewBag.Publishers = new SelectList(await _bookService.GetPublishers(), "PublisherId", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.AddBook(book);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Authors = new SelectList(await _bookService.GetAuthors(), "AuthorId", "Name");
            ViewBag.Categories = new SelectList(await _bookService.GetCategories(), "CategoryId", "Name");
            ViewBag.Publishers = new SelectList(await _bookService.GetPublishers(), "PublisherId", "Name");

            return View(book);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
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

            ViewBag.Authors = new SelectList(await _bookService.GetAuthors(), "AuthorId", "Name");
            ViewBag.Categories = new SelectList(await _bookService.GetCategories(), "CategoryId", "Name");
            ViewBag.Publishers = new SelectList(await _bookService.GetPublishers(), "PublisherId", "Name");

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bookService.UpdateBook(book);
                return RedirectToAction("Index", "Catalog");
            }

            ViewBag.Authors = new SelectList(await _bookService.GetAuthors(), "AuthorId", "Name");
            ViewBag.Categories = new SelectList(await _bookService.GetCategories(), "CategoryId", "Name");
            ViewBag.Publishers = new SelectList(await _bookService.GetPublishers(), "PublisherId", "Name");

            return View(book);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteBook(id);
            return RedirectToAction("Index", "Catalog");
        }

        [Authorize]
        public IActionResult AddToCart(int id)
        {
            return RedirectToAction("AddToCart", "Cos", new { id = id });
        }
    }
}