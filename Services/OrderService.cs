using LibrarieOnline.Models;
using LibrarieOnline.Repositories;

namespace LibrarieOnline.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _orderRepository.GetOrdersWithItems();
        }
        public async Task RemoveOrderItem(int orderItemId)
        {
            await _orderRepository.RemoveOrderItem(orderItemId);
        }

        public async Task<Order?> GetOrderById(int id)
        {
            var orders = await _orderRepository.GetOrdersWithItems();
            return orders.FirstOrDefault(o => o.OrderId == id);
        }

        public async Task DeleteOrder(int id)
        {
            await _orderRepository.Delete(id);
        }
    }
}