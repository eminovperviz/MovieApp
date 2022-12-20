namespace MovieApp.Application.Contracts.DTOs;

public class MovieEntityTableListResponse : BaseDTO
{
    public int Count { get; set; }
    public ICollection<MovieEntityTableResponse> Data { get; set; }

}