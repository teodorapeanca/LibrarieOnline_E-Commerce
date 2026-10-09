using LibrarieOnline.Models;
using LibrarieOnline.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarieOnline.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PublishersController : Controller
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        // GET: Publishers
        public async Task<IActionResult> Index()
        {
            var publishers = await _publisherService.GetAllPublishers();

            return View(publishers);
        }

        // GET: Publishers/Details/5
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

            return View(publisher);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublisherId,Name,Country")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                await _publisherService.AddPublisher(publisher);

                return RedirectToAction(nameof(Index));
            }

            return View(publisher);
        }

        // GET: Publishers/Edit/5
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

        // POST: Publishers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublisherId,Name,Country")] Publisher publisher)
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

        // GET: Publishers/Delete/5
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

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _publisherService.DeletePublisher(id);

            return RedirectToAction(nameof(Index));
        }
    }
}