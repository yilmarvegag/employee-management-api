using System.Linq.Expressions;

namespace EmployeeManagement.Application.Interfaces
{
    /// <summary>
    /// Define operaciones genéricas de acceso a datos para cualquier entidad.
    /// </summary>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Devuelve todas las entidades sin aplicar seguimiento de cambios (tracking).
        /// </summary>
        IQueryable<TEntity> FindAll();

        /// <summary>
        /// Devuelve las entidades que cumplen con la condición especificada.
        /// </summary>
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Devuelve las entidades que cumplen con la condición e incluye relaciones especificadas.
        /// </summary>
        IQueryable<TEntity> FindByConditionIncludes(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Agrega una nueva entidad al contexto.
        /// </summary>
        Task Create(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Agrega una colección de entidades al contexto de forma asincrónica.
        /// </summary>
        Task CreateRange(List<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica si existe alguna entidad que cumpla con el predicado especificado.
        /// </summary>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Marca una entidad como modificada para que sea actualizada al guardar los cambios.
        /// </summary>
        void Update(TEntity entity);

        /// <summary>
        /// Marca una colección de entidades como modificadas.
        /// </summary>
        void UpdateRange(List<TEntity> entities);

        /// <summary>
        /// Elimina una entidad del contexto.
        /// </summary>
        void Delete(TEntity entity);

        /// <summary>
        /// Elimina una colección de entidades del contexto.
        /// </summary>
        void DeleteRange(List<TEntity> entities);
    }
}
