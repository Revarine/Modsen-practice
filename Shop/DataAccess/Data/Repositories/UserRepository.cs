using Microsoft.EntityFrameworkCore;
using DataAccess.Data.Interfaces; 
using DataAccess.Models;

namespace DataAccess.Data.Repositories;

public class UserRepository : IRepository<User>
{
    private ShopDbContext _dbContext { get; set; }

    public UserRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetElementsAsync(CancellationToken cancellationToken = default)
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

    public async Task UpdateAsync(int id, User item, CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.Where(e => e.Id == id)
            .ExecuteUpdateAsync
            (s =>
                s
                    .SetProperty(e => e.Email, item.Email)
                    .SetProperty(e => e.Password, item.Password)
                    .SetProperty(e => e.Username, item.Username),
                cancellationToken
            );
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.Where(e => e.Id == id).ExecuteDeleteAsync(cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}