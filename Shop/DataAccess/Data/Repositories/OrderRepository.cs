using Microsoft.EntityFrameworkCore;
using DataAccess.Data.Interfaces;
using DataAccess.Models;

namespace DataAccess.Data.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private ShopDbContext _dbContext { get; set; }

        public OrderRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetElementsAsync(CancellationToken cancellationToken = default)
        {
            var orders = await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ToListAsync(cancellationToken);

            return orders.AsEnumerable();
        }

        public async Task<Order> GetItemAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task CreateAsync(Order item, CancellationToken cancellationToken = default)
        {
            await _dbContext.Orders.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(int id, Order item, CancellationToken cancellationToken = default)
        {
            var existingOrder = await _dbContext.Orders.FindAsync(new object[] { id }, cancellationToken);
            if (existingOrder != null)
            {
                existingOrder.UserId = item.UserId;
                existingOrder.User = item.User;
                existingOrder.OrderItems = item.OrderItems;

                _dbContext.Orders.Update(existingOrder);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var order = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

            if (order != null)
            {
                _dbContext.OrderItems.RemoveRange(order.OrderItems);  // Remove associated OrderItems
                _dbContext.Orders.Remove(order);  // Remove the Order itself
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
