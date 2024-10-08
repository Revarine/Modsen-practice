using Microsoft.EntityFrameworkCore;
using DataAccess.Data.Interfaces;
using DataAccess.Models;

namespace DataAccess.Data.Repositories;

public class ProductRepository : IRepository<Product>
{
    private ShopDbContext _dbContext { get; set; }

    public ProductRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<Product>> GetElementsAsync(CancellationToken cancellationToken = default)
    {
        var products = await _dbContext.Products.AsNoTracking().Include(x => x.Category).ToListAsync(cancellationToken);
        return products.AsEnumerable();
    }

    public async Task<Product> GetItemAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products.Where(x => x.Id == id).Include(x => x.Category).FirstAsync(cancellationToken);
    }

    public async Task CreateAsync(Product item, CancellationToken cancellationToken = default)
    {
        await _dbContext.Products.AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(int id, Product item, CancellationToken cancellationToken = default)
    {
        await _dbContext.Products.Where(e => e.Id == id).ExecuteUpdateAsync
        (s =>
                s
                    .SetProperty(e => e.Name, item.Name)
                    .SetProperty(e => e.Price, item.Price)
                    .SetProperty(e => e.CategoryId, item.CategoryId),
            cancellationToken
        );
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _dbContext.Products.Where(e => e.Id == id).ExecuteDeleteAsync(cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}