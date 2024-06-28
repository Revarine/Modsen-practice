using Microsoft.EntityFrameworkCore;
using Shop.Data.Interfaces;
using Shop.Models;

namespace Shop.Data.Repositories;

public class OrderItemsRepository : IRepository<OrderItem>
{
    private ShopDbContext _dbContext { get; set; }

    public OrderItemsRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<OrderItem>> GetIEnumerableAsync(CancellationToken cancellationToken = default)
    {
        var orderItems = await _dbContext.OrderItems.AsNoTracking().ToListAsync(cancellationToken);
        return orderItems;
    }

    public async Task<OrderItem> GetItemAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.OrderItems.FindAsync(id, cancellationToken);
    }

    public async Task CreateAsync(OrderItem item, CancellationToken cancellationToken = default)
    {
        await _dbContext.OrderItems.AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(OrderItem item, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var orderItem = await _dbContext.OrderItems.FindAsync(id, cancellationToken);
        if (orderItem != null)
        {
            _dbContext.Remove(orderItem);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

}