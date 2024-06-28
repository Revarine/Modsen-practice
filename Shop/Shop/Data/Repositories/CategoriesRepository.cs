using Microsoft.EntityFrameworkCore;
using Shop.Data.Interfaces;
using Shop.Models;

namespace Shop.Data.Repositories;

public class CategoriesRepository : IRepository<Category>
{
    private ShopDbContext _dbContext { get; set; }

    public CategoriesRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Category>> GetIEnumerableAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _dbContext.Categories.AsNoTracking().ToListAsync(cancellationToken);
        return categories;
    }

    public async Task<Category> GetItemAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Categories.FindAsync(id, cancellationToken);
    }

    public async Task CreateAsync(Category item, CancellationToken cancellationToken = default)
    {
        await _dbContext.Categories.AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(int id, Category item, CancellationToken cancellationToken = default)
    {
        await _dbContext.Categories.Where(e => e.Id == id).ExecuteUpdateAsync
        (s =>
            s
                .SetProperty(e => e.Name, item.Name),
            cancellationToken
        );
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _dbContext.Categories.Where(e => e.Id == id).ExecuteDeleteAsync(cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}