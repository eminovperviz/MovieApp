using System.Linq.Expressions;

namespace MovieApp.Domain.Interfaces;

public interface IQueryRepository<T> : IRepository where T : BaseEntity
{
    IQueryable<T> Table { get; }
    IQueryable<T> TableNoTracking { get; }
    T Find(Expression<Func<T, bool>> match);
    ICollection<T> FindAll(Expression<Func<T, bool>> match);
    Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default);
    IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
    Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default);
    T Get(object id);
    IQueryable<T> GetAll(bool isNoTracking = true);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    ValueTask<T> GetAsync(object id, CancellationToken cancellationToken = default);

}

public interface IRepository
{

}