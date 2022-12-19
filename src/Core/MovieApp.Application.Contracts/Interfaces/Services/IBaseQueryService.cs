namespace MovieApp.Application.Contracts.Interfaces;

public interface IBaseQueryService<T> where T : BaseEntity
{
    T Find(Expression<Func<T, bool>> match);
    ICollection<T> FindAll(Expression<Func<T, bool>> match);
    Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default);
    IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
    Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default);
    T Get(object id);
    IQueryable<T> GetAll(bool disableTracking = true);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    ValueTask<T> GetAsync(object id, CancellationToken cancellationToken = default);
    IQueryable<T> Filter(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int page, int pageSize);
    IQueryable<K> Filter<K>(IQueryable<K> query, Expression<Func<K, bool>> filter, Func<IQueryable<K>, IOrderedQueryable<K>> orderBy, int page, int pageSize);

}