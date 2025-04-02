namespace LogAndStore.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetListAsync(int page, int limit, CancellationToken cancellationToken = default);

        Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task CreateRangeAsync(List<TEntity> entity, CancellationToken cancellationToken = default);

        Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task RemoveRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task UpdateRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

        Task ClearTableAsync(CancellationToken cancellationToken = default);
    }
}