using Microsoft.EntityFrameworkCore;
using Shop.Data.Interfaces;
using Shop.Models;

namespace Shop.Data.Repositories;

public class ProductsRepository : IRepository<Product>
{
    private ShopDbContext _dbContext { get; set; }
    
    public ProductsRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<Product>> GetIEnumerableAsync(CancellationToken cancellationToken = default)
    {
        var products = await _dbContext.Products.AsNoTracking().ToListAsync(cancellationToken);
        return products.AsEnumerable();
    }

    public async Task<Product> GetItemAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products.FindAsync(id, cancellationToken);
    }

    public async Task CreateAsync(Product item, CancellationToken cancellationToken = default)
    {
        await _dbContext.Products.AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Product item, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _dbContext.Products.FindAsync(id, cancellationToken);
        if (product != null)
        {
            _dbContext.Remove(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}