using LibrarieOnline.Data;
using LibrarieOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarieOnline.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Order>> GetOrdersWithItems()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Book)
                .ToListAsync();
        }
        public async Task RemoveOrderItem(int orderItemId)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);

            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}