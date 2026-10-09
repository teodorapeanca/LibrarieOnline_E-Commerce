using LibrarieOnline.Models;

namespace LibrarieOnline.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<Order?> GetOrderById(int id);
        Task DeleteOrder(int id);
        Task RemoveOrderItem(int orderItemId);
    }
}