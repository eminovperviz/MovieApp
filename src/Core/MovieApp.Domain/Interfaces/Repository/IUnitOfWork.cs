using Microsoft.EntityFrameworkCore.Storage;

namespace MovieApp.Domain.Interfaces;

public interface IEFUnitOfWork
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    IDbContextTransaction BeginTransaction();
    int Commit();
    Task<int> CommitAsync();
    void Rollback();
    Task RollbackAsync();

    int ExecuteSqlRaw(string sqlQuery);
    Task<int> ExecuteSqlInterpolatedAsync(FormattableString sqlQuery);
    Task<int> ExecuteSqlRawAsync(string sqlQuery);
}
