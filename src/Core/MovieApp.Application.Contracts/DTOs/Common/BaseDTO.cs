namespace MovieApp.Application.Contracts.DTOs;

public class BaseDTO
{

}

public class BasePagingDTO<T> : BaseDTO
{
    public long Count { get; set; }

    public ICollection<T> Data { get; set; }
}

