using LogAndStore.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LogAndStore.DAL.Repositories
{
    public class BaseRepository<TEntity>(ApplicationDbContext dbContext) : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<List<TEntity>> GetListAsync(
            CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .Set<TEntity>()
                .ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetListAsync(
            int page,
            int limit,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .Set<TEntity>()
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);
        }

        public async Task CreateAsync(
            TEntity entity,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(entity, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task CreateRangeAsync(
            List<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.AddRangeAsync(entities, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(
            TEntity entity,
            CancellationToken cancellationToken = default)
        {
            _dbContext.Update(entity);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRangeAsync(
            ICollection<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            _dbContext.UpdateRange(entities);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(
            TEntity entity,
            CancellationToken cancellationToken = default)
        {
            _dbContext.Remove(entity);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveRangeAsync(
            ICollection<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            _dbContext.RemoveRange(entities);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task ClearTableAsync(CancellationToken cancellationToken = default)
        {
            var entityType = _dbContext.Model.FindEntityType(typeof(TEntity))
                ?? throw new InvalidOperationException($"Не удалось определить имя таблицы для типа {typeof(TEntity).Name}");
            var tableName = entityType.GetTableName();
            var sql = $"TRUNCATE TABLE \"{tableName}\" RESTART IDENTITY";

            await _dbContext.Database.ExecuteSqlRawAsync(sql, cancellationToken);
        }

        private async Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}