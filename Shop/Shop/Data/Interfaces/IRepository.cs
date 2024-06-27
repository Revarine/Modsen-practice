namespace Shop.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetIEnumerableAsync(CancellationToken cancellationToken = default);
        Task<T> GetItemAsync(int id, CancellationToken cancellationToken = default);
        Task CreateAsync(T item, CancellationToken cancellationToken = default);
        Task UpdateAsync(T item, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
