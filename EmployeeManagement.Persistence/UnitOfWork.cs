using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;
using EmployeeManagement.Application.Interfaces;

namespace EmployeeManagement.Persistence
{
    /// <summary>
    /// Provides a unit of work implementation for managing database transactions and saving changes within the Employee
    /// Management context. Ready for SQL reties, transient failures and cloud environments. Not thread-safe, intended for internal use within the data access layer.
    /// Best practice: Encapsulates transaction management and execution strategy to ensure reliable data operations, especially in scenarios with transient failures.
    /// Like to deadlock, timeouts and network failures. By using the execution strategy, it can automatically retry operations that fail due to transient issues, improving the resilience of the application when interacting with the database.
    /// </summary>
    /// <remarks>This class encapsulates transaction management and ensures that operations are executed with
    /// the configured execution strategy, which may include automatic retries for transient failures. It is intended
    /// for internal use within the data access layer and is not thread-safe.</remarks>
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeeManagementContext _dbContext;
        private readonly IExecutionStrategy _executionStrategy;

        public UnitOfWork(EmployeeManagementContext dbContext)
        {
            _dbContext = dbContext;
            _executionStrategy = dbContext.Database.CreateExecutionStrategy();
        }

        public async Task<T> ExecuteTransactionAsync<T>(Func<Task<T>> operation,CancellationToken cancellationToken = default)
        {
            return await _executionStrategy.ExecuteAsync(async () =>
            {
                await using var transaction =
                    await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var result = await operation();

                    await transaction.CommitAsync(cancellationToken);

                    return result;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            });
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _executionStrategy.ExecuteAsync(async () =>
            {
                return await _dbContext.SaveChangesAsync(cancellationToken);
            });
        }
    }
}
