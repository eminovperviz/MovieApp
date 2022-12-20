namespace MovieApp.Persistence.Data;

public class EFRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly MovieAppContext _context;
    private DbSet<T> _dbSet;
    public virtual IQueryable<T> Table => _dbSet;
    public virtual IQueryable<T> TableNoTracking => _dbSet.AsNoTracking();

    public EFRepository(MovieAppContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity, bool isCommit = true, CancellationToken cancellationToken = new CancellationToken())
    {

        await _dbSet.AddAsync(entity, cancellationToken);

        await SaveChangesAsync(isCommit, cancellationToken);
    }

    public object GetTempId(T entity) => _context.Entry(entity).Property("Id").CurrentValue;

    public async Task AddRangeAsync(IEnumerable<T> entities, bool isCommit = true)
    {
        _dbSet.AddRange(entities);
        await SaveChangesAsync(isCommit);
    }

    public void Attach(T entity) => _dbSet.Attach(entity);

    public void AttachRange(IEnumerable<T> entities) => _dbSet.AttachRange(entities);

    public async Task DeleteAsync(T entity, bool isCommit = true)
    {
        _dbSet.Remove(entity);
        await SaveChangesAsync(isCommit);
    }

    public async Task DeleteAsync(T entity, bool isCommit = true, CancellationToken cancellationToken = new CancellationToken())
    {
        _dbSet.Remove(entity);
        await SaveChangesAsync(isCommit, cancellationToken);
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities, bool isCommit = true)
    {
        _dbSet.RemoveRange(entities);
        await SaveChangesAsync(isCommit);
    }

    public async Task UpdateAsync(T entity, bool isCommit = true, CancellationToken cancellationToken = new CancellationToken())
    {
        _dbSet.Update(entity);
        await SaveChangesAsync(isCommit, cancellationToken);
    }

    public Task<int> SaveChangesAsync(bool isCommit = true, CancellationToken cancellationToken = new CancellationToken()) => _context.SaveChangesAsync(isCommit, cancellationToken);

    public T Find(Expression<Func<T, bool>> match) => Table.SingleOrDefault(match);

    public ICollection<T> FindAll(Expression<Func<T, bool>> match) => Table.Where(match).ToList();

    /// <summary>
    /// Don't use multiple async/await operation on same context. SEE link
    /// https://aka.ms/efcore-docs-threading
    /// </summary>
    /// <returns></returns>
    public Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default) => Table.Where(match).ToListAsync(cancellationToken);

    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default) => Table.FirstOrDefaultAsync(match, cancellationToken);

    public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate) => Table.Where(predicate);

    public Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) => Table.Where(predicate).ToListAsync(cancellationToken);

    public T Get(object id) => _dbSet.Find(id);

    public IQueryable<T> GetAll(bool isNoTracking = true) => isNoTracking ? TableNoTracking : Table;

    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default) => Table.ToListAsync(cancellationToken);

    public ValueTask<T> GetAsync(object id, CancellationToken cancellationToken = default) => _dbSet.FindAsync(new object[] { id }, cancellationToken);

    public IQueryable<T> IncludeMany(params Expression<Func<T, object>>[] includes)
    {
        if (includes != null)
        {
            _dbSet = includes.Aggregate(_dbSet, (current, include) => (DbSet<T>)current.Include(include));
        }
        return _dbSet;
    }

    public IQueryable<T> IncludeMany(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        if (includes != null)
        {
            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        return query.Where(predicate);
    }



    public async Task UpdateSpecialPropertiesAsync(T entity, bool isCommit = true, params Expression<Func<T, object>>[] properties)
    {
        var entry = _context.Entry(entity);

        entry.State = EntityState.Unchanged;

        foreach (var prop in properties)
        {
            entry.Property(prop).IsModified = true;
        }

        await SaveChangesAsync(isCommit);
    }

    public async Task UpdateExceptedPropertiesAsync(T entity, bool isCommit = false, params Expression<Func<T, object>>[] properties)
    {
        var entry = _context.Entry(entity);

        entry.State = EntityState.Modified;

        foreach (var prop in properties)
        {
            entry.Property(prop).IsModified = false;
        }

        await SaveChangesAsync(isCommit);
    }
}

