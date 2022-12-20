namespace MovieApp.Application.Contracts.DTOs;

public class MovieEntityAddRequest : BaseDTO, IMapTo<MovieEntity>
{
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public int Rating { get; set; }
    public string Synopsis { get; set; }

}