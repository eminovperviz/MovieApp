using Microsoft.EntityFrameworkCore.Storage;

namespace MovieApp.Persistence.Data;

public class EFUnitOfWork : IEFUnitOfWork
{
    private readonly MovieAppContext _dbContext;
    public EFUnitOfWork(MovieAppContext dbContext) => _dbContext = dbContext;

    public Task<IDbContextTransaction> BeginTransactionAsync() => _dbContext.Database.BeginTransactionAsync();
    public IDbContextTransaction BeginTransaction() => _dbContext.Database.BeginTransaction();

    public int ExecuteSqlRaw(string sqlQuery) => _dbContext.Database.ExecuteSqlRaw(sqlQuery);

    public Task<int> ExecuteSqlRawAsync(string sqlQuery) => _dbContext.Database.ExecuteSqlRawAsync(sqlQuery);

    public Task<int> ExecuteSqlInterpolatedAsync(FormattableString sqlQuery) => _dbContext.Database.ExecuteSqlInterpolatedAsync(sqlQuery);

    public int Commit() => _dbContext.SaveChanges(true);

    public Task<int> CommitAsync() => _dbContext.SaveChangesAsync(true);

    public void Rollback() => _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());

    public async Task RollbackAsync()
    {
        foreach (var entity in _dbContext.ChangeTracker.Entries())
        {
            await entity.ReloadAsync();
        }
    }
}