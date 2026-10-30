
using LibrarieOnline.Models;
using LibrarieOnline.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarieOnline.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class PublishersController : Controller
    {
        private readonly IPublisherService _publisherService;
        private readonly IBookService _bookService;

        public PublishersController(
            IPublisherService publisherService,
            IBookService bookService)
        {
            _publisherService = publisherService;
            _bookService = bookService;
        }

        // Admin si User pot vedea lista editurilor
        public async Task<IActionResult> Index()
        {
            var publishers = await _publisherService.GetAllPublishers();
            return View(publishers);
        }

        // Admin si User pot vedea cartile unei edituri
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _publisherService.GetPublisherById(id.Value);

            if (publisher == null)
            {
                return NotFound();
            }

            var allBooks = await _bookService.GetAllBooks();

            var publisherBooks = allBooks
                .Where(b => b.PublisherId == id.Value)
                .ToList();

            ViewBag.PublisherBooks = publisherBooks;

            return View(publisher);
        }

        // Doar Admin poate adauga edituri
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("PublisherId,Name,Country")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                await _publisherService.AddPublisher(publisher);
                return RedirectToAction(nameof(Index));
            }

            return View(publisher);
        }

        // Doar Admin poate edita edituri
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _publisherService.GetPublisherById(id.Value);

            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("PublisherId,Name,Country")] Publisher publisher)
        {
            if (id != publisher.PublisherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _publisherService.UpdatePublisher(publisher);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _publisherService.PublisherExists(publisher.PublisherId))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(publisher);
        }

        // Doar Admin poate sterge edituri
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _publisherService.GetPublisherById(id.Value);

            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _publisherService.DeletePublisher(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
