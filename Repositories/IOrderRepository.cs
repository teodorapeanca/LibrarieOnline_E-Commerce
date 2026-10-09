using LibrarieOnline.Models;

namespace LibrarieOnline.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetOrdersWithItems();
        Task RemoveOrderItem(int orderItemId);
    }
}