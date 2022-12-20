namespace MovieApp.Application.Contracts.Interfaces;

public interface IMovieEntityService : IBaseEntityService<MovieEntity>
{
    Task<MovieEntityTableListResponse> GetTableAsync(PagingRequest pagingRequest);
    Task<List<MovieEntityDTO>> GetTableByTitleAsync(string title);
    Task<List<MovieEntityDTO>> GetTableByReleaseYearAsync(int releaseYear);
    Task<MovieEntityUpdateRequest> GetForUpdateByIdAsync(int id);
    Task<MovieEntityDTO> AddAsync(MovieEntityAddRequest model);
    Task EditAsync(MovieEntityUpdateRequest model);
    Task DeleteByIdAsync(int id);
    Task<MovieEntityDTO> GetByIdAsync(int id);

}
