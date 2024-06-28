using Microsoft.EntityFrameworkCore;
using Shop.Data.Interfaces;
using Shop.Models;

namespace Shop.Data.Repositories;

public class UserRepository : IRepository<User>
{
    private ShopDbContext _dbContext { get; set; }

    public UserRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetIEnumerableAsync(CancellationToken cancellationToken = default)
    {
        var users = await _dbContext.Users.AsNoTracking().ToListAsync(cancellationToken);
        return users;
    }

    public async Task<User> GetItemAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.FindAsync(id, cancellationToken);
    }

    public async Task CreateAsync(User item, CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(User item, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users.FindAsync(id, cancellationToken);
        if (user != null)
        {
            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

}