namespace EmployeeManagement.Application.Interfaces
{
    /// <summary>
    /// Defines a contract for coordinating changes and transactional operations within a data context. Provides methods
    /// to persist changes and execute operations within a transaction scope asynchronously.
    /// </summary>
    /// <remarks>Implementations of this interface are typically used to ensure that multiple data operations
    /// are committed as a single unit, supporting atomicity and consistency. The interface is intended for use in
    /// scenarios where transactional integrity and asynchronous operations are required, such as in repository or data
    /// access patterns.</remarks>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Asynchronously saves all changes made in the context to the underlying database.
        /// </summary>
        /// <remarks>If the operation is canceled, the returned task will be in the Canceled state. This
        /// method should be used when performing database operations that may take time, to avoid blocking the calling
        /// thread.</remarks>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the save operation.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries
        /// written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Executes the specified asynchronous operation within a database transaction and returns its result.
        /// </summary>
        /// <remarks>If the operation completes successfully, the transaction is committed. If the
        /// operation fails or is canceled, the transaction is rolled back. The operation should not perform its own
        /// transaction management.</remarks>
        /// <typeparam name="T">The type of the result produced by the operation.</typeparam>
        /// <param name="operation">A function representing the asynchronous operation to execute within the transaction. The operation must
        /// return a task that produces a result of type <typeparamref name="T"/>.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the transaction operation. The default value is <see
        /// cref="CancellationToken.None"/>.</param>
        /// <returns>A task that represents the asynchronous transaction operation. The task result contains the value returned
        /// by the operation.</returns>
        Task<T> ExecuteTransactionAsync<T>(Func<Task<T>> operation, CancellationToken cancellationToken = default);
    }
}
