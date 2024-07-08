using Microsoft.EntityFrameworkCore;
using DataAccess.Data.Interfaces;
using DataAccess.Models;

namespace DataAccess.Data.Repositories;

public class CategoryRepository : IRepository<Category>
{
    private ShopDbContext _dbContext { get; set; }

    public CategoryRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Category>> GetElementsAsync(CancellationToken cancellationToken = default)
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