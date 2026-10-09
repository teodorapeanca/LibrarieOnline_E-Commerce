using LibrarieOnline.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarieOnline.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrders();
            return View(orders);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderService.DeleteOrder(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            await _orderService.RemoveOrderItem(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Checkout()
        {
            TempData["Success"] = "Comanda a fost plasată cu succes!";
            return RedirectToAction(nameof(Index));
        }
    }
}