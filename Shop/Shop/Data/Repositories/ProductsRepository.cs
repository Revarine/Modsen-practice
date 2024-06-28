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


    public Task<IEnumerable<Product>> GetIEnumerableAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetItemAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Product item, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Product item, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}