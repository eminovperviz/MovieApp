namespace MovieApp.Domain.Entities;

public class MovieEntity : BaseEntity<int>
{
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public int Rating { get; set; }
    public string Synopsis { get; set; }
}
