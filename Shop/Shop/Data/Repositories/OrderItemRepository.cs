using Microsoft.EntityFrameworkCore;
using Shop.Data.Interfaces;
using Shop.Models;

namespace Shop.Data.Repositories;

public class OrderItemRepository : IRepository<OrderItem>
{
    private ShopDbContext _dbContext { get; set; }

    public OrderItemRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<OrderItem>> GetElementsAsync(CancellationToken cancellationToken = default)
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

    public async Task UpdateAsync(int id, OrderItem item, CancellationToken cancellationToken = default)
    {
        await _dbContext.OrderItems.Where(e => e.Id == id).ExecuteUpdateAsync
            ( s =>
                s
                    .SetProperty(e => e.OrderId, item.OrderId)
                    .SetProperty(e => e.ProductId, item.ProductId)
                    .SetProperty(e => e.Quantity, item.Quantity),
                cancellationToken
                );
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _dbContext.OrderItems.Where(e => e.Id == id).ExecuteDeleteAsync(cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}