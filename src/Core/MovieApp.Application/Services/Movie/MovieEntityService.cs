namespace MovieApp.Application.Services.Movie;

public class MovieEntityService : BaseEntityService<MovieEntity>, IMovieEntityService
{
    private readonly IMapper _mapper;
    private readonly IMovieEntityRepository _repository;


    public MovieEntityService(IMapper mapper, IMovieEntityRepository repository) : base(repository)
    {
        _mapper = mapper;
        _repository = repository;

    }


    public Task<List<MovieEntityDTO>> GetTableByTitleAsync(string title)
    {
        return _repository.GetAll().Where(x => x.Title == title).Select(entity => new MovieEntityDTO
        {
            Title = entity.Title,
            ReleaseYear = entity.ReleaseYear,
            Rating = entity.Rating,
            Synopsis = entity.Synopsis,
            Id = entity.Id,


        }).ToListAsync();

    }
    public Task<List<MovieEntityDTO>> GetTableByReleaseYearAsync(int releaseYear)
    {
        return _repository.GetAll().Where(x => x.ReleaseYear == releaseYear).Select(entity => new MovieEntityDTO
        {
            Title = entity.Title,
            ReleaseYear = entity.ReleaseYear,
            Rating = entity.Rating,
            Synopsis = entity.Synopsis,
            Id = entity.Id,


        }).ToListAsync();

    }
    public async Task<MovieEntityTableListResponse> GetTableAsync(PagingRequest pagingRequest)
    {
        var query = from o in _repository.GetAll()

                    select new MovieEntityTableResponse
                    {
                        Title = o.Title,
                        ReleaseYear = o.ReleaseYear,
                        Rating = o.Rating,
                        Synopsis = o.Synopsis,
                        Id = o.Id,

                    };

        query = AddFilter(query, pagingRequest, out int count);

        return new MovieEntityTableListResponse { Count = count, Data = await query.ToListAsync() };
    }

    public async Task<MovieEntityUpdateRequest> GetForUpdateByIdAsync(int id)
    {
        var entity = await GetAsync(id);
        var model = _mapper.Map<MovieEntityUpdateRequest>(entity);

        return model;
    }
    public async Task<MovieEntityDTO> AddAsync(MovieEntityAddRequest model)
    {
        var entity = _mapper.Map<MovieEntity>(model);

        await AddAsync(entity);

        var dto = _mapper.Map<MovieEntityDTO>(entity);

        return dto;
    }

    public async Task EditAsync(MovieEntityUpdateRequest model)
    {
        var entity = _mapper.Map<MovieEntity>(model);

        await UpdateAsync(entity);
    }

    public async Task DeleteByIdAsync(int id)
    {
        MovieEntity entity = new() { Id = id };

        await DeleteAsync(entity);
    }

    public async Task<MovieEntityDTO> GetByIdAsync(int id)
    {
        var entity = await GetAsync(id);

        return _mapper.Map<MovieEntityDTO>(entity);
    }

}

