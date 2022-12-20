namespace MovieApp.Infrastructure.Repositories;

public class MovieEntityRepository : EFRepository<MovieEntity>, IMovieEntityRepository
{
    public MovieEntityRepository(MovieAppContext context) : base(context)
    {

    }
}
