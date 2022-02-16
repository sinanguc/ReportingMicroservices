using Contact.Application.Contracts.Persistence;
using Contact.Domain.Common;
using Contact.Domain.Entities;
using Contact.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IAsyncRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected readonly ContactContext _dbContext;

        public BaseRepository(ContactContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, 
            IOrderedQueryable<TEntity>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (disableTracking)
                query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString))
                query = query.Include(includeString);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync(cancellationToken);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, 
            IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (disableTracking)
                query = query.AsNoTracking();

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync(cancellationToken);

            return await query.ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id, cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id, cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? 
                await _dbContext.Set<TEntity>().FirstOrDefaultAsync(cancellationToken: cancellationToken).ConfigureAwait(false) 
                : await _dbContext.Set<TEntity>().FirstOrDefaultAsync(filter, cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> LastOrDefaultAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ?
                await _dbContext.Set<TEntity>().LastOrDefaultAsync(cancellationToken: cancellationToken).ConfigureAwait(false)
                : await _dbContext.Set<TEntity>().LastOrDefaultAsync(filter, cancellationToken).ConfigureAwait(false);
        }

        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public void DetachAll()
        {
            _dbContext.ChangeTracker.Clear();
        }

        #region Disposable
        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _dbContext.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseRepository()
        {
            Dispose(true);
        }
        #endregion

    }
}
