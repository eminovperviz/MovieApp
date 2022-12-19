namespace MovieApp.Application.Exceptions;

public class ApiException : ApplicationException
{
    public int? Status { get; set; }

    public ApiException(HttpResponseStatusType type, string? message = null) : base(message ?? type.GetDescription()) => Status = (int)type;

    public ApiException(string? message = null) : base(message)
    {
    }
}
