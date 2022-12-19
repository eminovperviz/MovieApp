using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MovieApp.Application.Exceptions;

public class ValidationFailedResult : ObjectResult
{
    public ValidationFailedResult(ModelStateDictionary modelState) : base(new ValidationResultModel(modelState))
    {
        StatusCode = StatusCodes.Status400BadRequest;
    }
}

public class ValidationResultModel
{
    public string Title { get; set; } = HttpResponseStatusType.ValidationError.GetDescription();
    public int Status { get; set; } = (int)HttpResponseStatusType.ValidationError;
    public IReadOnlyList<ValidationError> Errors { get; }
    public ValidationResultModel(ModelStateDictionary modelState)
    {
        var jsonErrors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => x.ErrorMessage))
                .ToList();

        Errors = jsonErrors.Select(x => JsonSerializer.Deserialize<ValidationError>(x) ?? new ValidationError()).ToList();
    }
}

public class ValidationError
{
    public string FieldName { get; set; }
    public string ValidationType { get; set; }
    public string? Pattern { get; set; }
    public string Message { get; set; }
}

