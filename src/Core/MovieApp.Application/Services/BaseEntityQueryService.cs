namespace MovieApp.Application.Services;

public class BaseEntityQueryService<T> : IBaseQueryService<T> where T : BaseEntity
{

    private readonly IQueryRepository<T> _repository;

    public BaseEntityQueryService(IQueryRepository<T> repository) => _repository = repository;
    public T Find(Expression<Func<T, bool>> match) => _repository.Find(match);

    public ICollection<T> FindAll(Expression<Func<T, bool>> match) => _repository.FindAll(match).ToList();

    public Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default) => _repository.FindAllAsync(match, cancellationToken);

    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default) => _repository.FirstOrDefaultAsync(match, cancellationToken);

    public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate) => _repository.FindBy(predicate);

    public Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) => _repository.FindByAsync(predicate, cancellationToken);

    public T Get(object id) => _repository.Get(id);

    public IQueryable<T> GetAll(bool disableTracking = true) => _repository.GetAll(disableTracking);

    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default) => _repository.GetAllAsync(cancellationToken);

    public ValueTask<T> GetAsync(object id, CancellationToken cancellationToken = default) => _repository.GetAsync(id, cancellationToken);

    public IQueryable<T> Filter(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int page, int pageSize)
    {
        IQueryable<T> query = _repository.GetAll();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        query = query.Skip((page - 1) * pageSize).Take(pageSize);

        return query;
    }

    public IQueryable<K> Filter<K>(IQueryable<K> query, Expression<Func<K, bool>> filter, Func<IQueryable<K>, IOrderedQueryable<K>> orderBy, int page, int pageSize)
    {

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        query = query.Skip((page) * pageSize).Take(pageSize);
        return query;
    }

    protected IQueryable<K> AddFilter<K>(IQueryable<K> query, PagingRequest pagingRequest, out int count)
    {
        if (pagingRequest.Filters is not null)
        {
            foreach (var filter in pagingRequest.Filters!)
            {
                string filterFieldName = string.Empty;

                object filterValue = null;

                filterValue = filter.Value;
                filterFieldName = filter.FieldName;

                string body = string.Empty;

                if (filter.EqualityType == "StartsWith")
                {
                    body += $"{filterFieldName}.StartsWith(\"{filterValue}\")";
                }
                else if (filter.EqualityType == "Contains")
                {
                    body += $"{filterFieldName}.Contains(\"{filterValue}\")";

                }
                else if (filter.EqualityType == "Equal")
                {
                    if (filterValue is int || ((JsonElement)filterValue).ValueKind == JsonValueKind.Number)
                        body += $"{filterFieldName}=={filterValue}";

                    else if (((JsonElement)filterValue).ValueKind == JsonValueKind.String)
                    {
                        if (filterValue.ToString().Equals("null"))
                            body += $"{filterFieldName} is null";
                        else
                            body += $"{filterFieldName}==\"{filterValue}\"";
                    }


                    else if (((JsonElement)filterValue).ValueKind == JsonValueKind.True)
                        body += $"{filterFieldName}";

                    else if (((JsonElement)filterValue).ValueKind == JsonValueKind.False)
                        body += $"{filterFieldName}=={filterValue?.ToString()?.ToLower()}";

                    else if (((JsonElement)filterValue).ValueKind == JsonValueKind.Null)
                        body += $"{filterFieldName} is null";

                }
                query = query.WhereDynamic($"x => x.{body}");
            }
        }

        count = query.Count();

        query = query.Skip((pagingRequest.Page) * pagingRequest.PageSize).Take(pagingRequest.PageSize);

        return query;
    }

}