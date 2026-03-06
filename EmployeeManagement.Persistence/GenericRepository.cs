using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeManagement.Persistence
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal EmployeeManagementContext _context;
        internal DbSet<TEntity> _dbSet;

        public GenericRepository(EmployeeManagementContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Returns a queryable collection of all entities of type TEntity without tracking changes in the context.
        /// </summary>
        /// <remarks>Use this method when you need to read entities without modifying them, as disabling
        /// change tracking can improve performance for read-only operations.</remarks>
        /// <returns>An IQueryable<TEntity> that can be used to enumerate all entities of type TEntity in the data store. The
        /// returned entities are not tracked by the context.</returns>
        public IQueryable<TEntity> FindAll()
        {
            //reduce overhead of EF, when use AsNoTracking
            return _dbSet.AsNoTracking();
        }

        /// <summary>
        /// Returns a queryable collection of entities that satisfy the specified predicate.
        /// </summary>
        /// <remarks>The returned query is not executed until it is enumerated. Because the results are
        /// not tracked, changes to the returned entities will not be persisted unless they are attached to the
        /// context.</remarks>
        /// <param name="expression">An expression that defines the conditions to filter the entities.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/> containing entities that match the specified condition. The results are
        /// not tracked by the context.</returns>
        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking();
        }

        /// <summary>
        /// Returns a queryable collection of entities that satisfy the specified condition, including the specified
        /// related entities.
        /// </summary>
        /// <remarks>The returned query is not tracked by the context, so changes to the entities will not
        /// be detected or persisted. Use this method when you need to read data without tracking for improved
        /// performance.</remarks>
        /// <param name="expression">An expression that defines the filter condition to apply to the entities.</param>
        /// <param name="includes">One or more expressions that specify the related entities to include in the query results. Each expression
        /// identifies a navigation property to be eagerly loaded.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/> containing entities that match the specified condition, with the
        /// specified related entities included. The results are not tracked by the context.</returns>
        public IQueryable<TEntity> FindByConditionIncludes(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.Where(expression).AsNoTracking();
        }

        /// <summary>
        /// Asynchronously adds a new entity to the underlying data set.
        /// </summary>
        /// <param name="entity">The entity to add to the data set. Cannot be null.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous add operation.</returns>
        public async Task Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Asynchronously adds a collection of entities to the context for insertion into the database.
        /// </summary>
        /// <remarks>The entities will be marked as added and inserted into the database when the changes
        /// are saved. This method does not save changes to the database; call SaveChangesAsync to persist the
        /// additions.</remarks>
        /// <param name="entities">The list of entities to add to the context. Cannot be null.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation. The default value is <see
        /// cref="CancellationToken.None"/>.</param>
        /// <returns>A task that represents the asynchronous add operation.</returns>
        public async Task CreateRange(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        /// <summary>
        /// Asynchronously determines whether any entities in the set satisfy the specified condition.
        /// </summary>
        /// <param name="predicate">An expression that defines the condition to test each entity for a match.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if any
        /// entities satisfy the condition; otherwise, <see langword="false"/>.</returns>
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// Updates the specified entity in the data set and marks it as modified for persistence.
        /// </summary>
        /// <remarks>The changes to the entity are not persisted to the database until SaveChanges is
        /// called on the context. If the entity is not being tracked, it will be attached to the context and marked as
        /// modified.</remarks>
        /// <param name="entity">The entity instance to update. Must not be null.</param>
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Updates the specified collection of entities in the data store.
        /// </summary>
        /// <remarks>This method marks all provided entities as modified so that their changes are saved
        /// to the database on the next save operation. If any entity is not already tracked, it will be attached to the
        /// context in the modified state.</remarks>
        /// <param name="entities">The list of entities to update. Cannot be null. Each entity must be tracked by the context or attached
        /// before calling this method.</param>
        public void UpdateRange(List<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        /// <summary>
        /// Removes the specified entity from the context, marking it for deletion from the database on the next save
        /// operation.
        /// </summary>
        /// <remarks>The entity will not be deleted from the database until changes are saved, typically
        /// by calling SaveChanges. If the entity is not being tracked by the context, this method may have no
        /// effect.</remarks>
        /// <param name="entity">The entity to remove from the context. Cannot be null.</param>
        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Removes the specified entities from the context, marking them for deletion in the database upon saving
        /// changes.
        /// </summary>
        /// <remarks>This method marks each entity in the provided list for deletion. The actual removal
        /// from the database occurs when SaveChanges is called on the context. If any entity is not being tracked by
        /// the context, it will be attached and then marked for deletion.</remarks>
        /// <param name="entities">The list of entities to be removed from the context. Cannot be null.</param>
        public void DeleteRange(List<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
