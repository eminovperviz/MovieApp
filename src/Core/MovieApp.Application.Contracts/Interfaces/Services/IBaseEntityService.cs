namespace MovieApp.Application.Contracts.Interfaces;

public interface IBaseEntityService<T> : IBaseQueryService<T> where T : BaseEntity
{
    Task AddAsync(T entity, bool isCommit = true, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<T> entities, bool isCommit = true);
    void Attach(T entity);
    void AttachRange(IEnumerable<T> entities);
    Task DeleteAsync(T entity, bool isCommit = true, CancellationToken cancellationToken = default);
    Task DeleteRangeAsync(IEnumerable<T> entities, bool isCommit = true);
    Task UpdateAsync(T entity, bool isCommit = true, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(bool isCommit = true, CancellationToken cancellationToken = new CancellationToken());
    IQueryable<T> IncludeMany(params Expression<Func<T, object>>[] includes);
    IQueryable<T> IncludeMany(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    Task UpdateSpecialPropertiesAsync(T entity, bool isCommit = true, params Expression<Func<T, object>>[] properties);
    Task UpdateExceptedPropertiesAsync(T entity, bool isCommit = false, params Expression<Func<T, object>>[] properties);
}
