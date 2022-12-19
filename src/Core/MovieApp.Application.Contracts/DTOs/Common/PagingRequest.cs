namespace MovieApp.Application.Contracts.DTOs;

public class PagingRequest : BaseDTO
{
    public PagingRequest()
    {
        Filters = new ();
    }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 15;
    public List<PagingRequestFilter> Filters { get; set; }
}


public sealed class PagingRequestFilter : BaseDTO
{
    public object Value { get; set; }
    public string FieldName { get; set; }
    public string EqualityType { get; set; }
}

public enum EqualityType
{
    Equal,
    StartsWith,
    Contains,
    GreaterThan,
    LessThan,
    GreaterThanOrEqual,
    LessThanOrEqual,
}