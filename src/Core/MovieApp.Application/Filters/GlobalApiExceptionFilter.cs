using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MovieApp.Application.Filters;

public class GlobalApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ApiException apiException)
        {
            HandleApiExceptions(context, apiException);
        }
        else if (context.Exception is OperationCanceledException)
        {
            HandleCancellationTokenException(context);
        }
        else if (context.Exception is DbUpdateException dbUpdateException)
        {
            HandleSqlExceptions(context, dbUpdateException);
        }
        else
        {
            UnHandledException(context);
        }
        context.ExceptionHandled = true;
    }

    private void HandleApiExceptions(ExceptionContext context, ApiException apiException)
    {
        context.Result = new ObjectResult(new
        {
            apiException.Status,
            apiException.Message
        });
        context.HttpContext.Response.StatusCode = apiException.Status == (int)HttpResponseStatusType.Unauthorized ? (int)HttpStatusCode.Unauthorized : (int)HttpStatusCode.BadRequest;

        //Log.Warning(apiException.Message);
    }

    private void HandleCancellationTokenException(ExceptionContext context)
    {
        var canceledException = new ApiException(HttpResponseStatusType.OperationCancelled);
        context.Result = new ObjectResult(new
        {
            canceledException.Status,
            canceledException.Message
        });

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }

    private void HandleSqlExceptions(ExceptionContext context, DbUpdateException dbUpdateException)
    {
        if (context.Exception?.InnerException is null)
            UnHandledException(context);
        ApiException apiException = new(HttpResponseStatusType.DbException);

        if (dbUpdateException!.InnerException!.Message.Contains("FOREIGN KEY"))
        {
            apiException = new ApiException(HttpResponseStatusType.ForeignKeyException);
        }
        else if (dbUpdateException.InnerException.Message.Contains("UNIQUE KEY"))
        {
            apiException = new ApiException(HttpResponseStatusType.DuplicateKeyException);
        }
        else if (dbUpdateException.InnerException.Message.Contains("REFERENCE constraint"))
        {
            apiException = new ApiException(HttpResponseStatusType.ReferfenceConstraintException);
        }
        context.Result = new ObjectResult(new
        {
            apiException.Status,
            apiException.Message
        });
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        //Log.Error(context.Exception?.InnerException, context.Exception?.InnerException?.Message);
    }

    private void UnHandledException(ExceptionContext context)
    {
        context.Result = new ObjectResult(new
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Message = context.Exception?.Message ?? context.Exception?.InnerException?.Message,
            //#if DEBUG 
            ExceptionDetails = context.Exception?.StackTrace
            //#endif
        });
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        //Log.Error(context.Exception, "UnHandledException");
    }
}


