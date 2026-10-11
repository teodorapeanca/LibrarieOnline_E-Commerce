
using LibrarieOnline.Models;
using LibrarieOnline.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarieOnline.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IBookService _bookService;

        public CategoriesController(
            ICategoryService categoryService,
            IBookService bookService)
        {
            _categoryService = categoryService;
            _bookService = bookService;
        }

        // Toata lumea poate vedea categoriile
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategories();
            return View(categories);
        }

        // Toata lumea poate vedea cartile unei categorii
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategoryById(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            var allBooks = await _bookService.GetAllBooks();

            var categoryBooks = allBooks
                .Where(b => b.CategoryId == id.Value)
                .ToList();

            ViewBag.CategoryBooks = categoryBooks;

            return View(category);
        }

        // Doar Admin poate crea categorii
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("CategoryId,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // Doar Admin poate edita categorii
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategoryById(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("CategoryId,Name")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingCategory =
                        await _categoryService.GetCategoryById(category.CategoryId);

                    if (existingCategory == null)
                    {
                        return NotFound();
                    }

                    existingCategory.Name = category.Name;

                    await _categoryService.UpdateCategory(existingCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _categoryService.CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // Doar Admin poate sterge categorii
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategoryById(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
