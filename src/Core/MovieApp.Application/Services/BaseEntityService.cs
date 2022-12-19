namespace MovieApp.Application.Services;

public class BaseEntityService<T> : BaseEntityQueryService<T>, IBaseEntityService<T> where T : BaseEntity
{
    private readonly IRepository<T> _repository;

    public BaseEntityService(IRepository<T> repository) : base(repository) => _repository = repository;

    public Task AddAsync(T entity, bool isCommit = true, CancellationToken cancellationToken = new CancellationToken()) => _repository.AddAsync(entity, isCommit, cancellationToken);

    public Task AddRangeAsync(IEnumerable<T> entities, bool isCommit = true) => _repository.AddRangeAsync(entities, isCommit);

    public void Attach(T entity) => _repository.Attach(entity);

    public void AttachRange(IEnumerable<T> entities) => _repository.AttachRange(entities);

    public Task DeleteAsync(T entity, bool isCommit = true, CancellationToken cancellationToken = new CancellationToken()) => _repository.DeleteAsync(entity, isCommit, cancellationToken);

    public virtual Task DeleteRangeAsync(IEnumerable<T> entities, bool isCommit = true) => _repository.DeleteRangeAsync(entities, isCommit);

    public Task UpdateAsync(T entity, bool isCommit = true, CancellationToken cancellationToken = new CancellationToken()) => _repository.UpdateAsync(entity, isCommit, cancellationToken);

    public Task<int> SaveChangesAsync(bool isCommit = true, CancellationToken cancellationToken = new CancellationToken()) => _repository.SaveChangesAsync(isCommit, cancellationToken);

    public IQueryable<T> IncludeMany(params Expression<Func<T, object>>[] includes) => _repository.IncludeMany(includes);

    public IQueryable<T> IncludeMany(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) => _repository.IncludeMany(predicate, includes);

    public Task UpdateSpecialPropertiesAsync(T entity, bool isCommit = true, params Expression<Func<T, object>>[] properties) => _repository.UpdateSpecialPropertiesAsync(entity, isCommit, properties);

    public Task UpdateExceptedPropertiesAsync(T entity, bool isCommit = false, params Expression<Func<T, object>>[] properties) => _repository.UpdateExceptedPropertiesAsync(entity, isCommit, properties);
}
