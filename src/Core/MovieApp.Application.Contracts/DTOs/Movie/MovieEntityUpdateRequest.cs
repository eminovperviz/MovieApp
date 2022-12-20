namespace MovieApp.Application.Contracts.DTOs;

public class MovieEntityUpdateRequest : BaseDTO, IMapTo<MovieEntity>
{
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public int Rating { get; set; }
    public string Synopsis { get; set; }
    public int Id { get; set; }

}