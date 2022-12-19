namespace MovieApp.Application.Extensions;

public static class AppGuard
{
    public static void ThrowIfNull<T>(this T value, string message)
    {
        if (value is null)
            throw new ApiException(HttpResponseStatusType.ValidationError, message);
    }

    public static void ThrowIfNull<T>(this T value, HttpResponseStatusType exceptionStatusType)
    {
        if (value is null)
            throw new ApiException(exceptionStatusType);
    }

    public static void ThrowIfNotNull<T>(this T value, HttpResponseStatusType exceptionStatusType)
    {
        if (value is not null)
            throw new ApiException(exceptionStatusType);
    }

    public static void ThrowIfTrue(bool value, HttpResponseStatusType httpResponseStatusType)
    {
        if (value)
        {
            throw new ApiException(httpResponseStatusType);
        }
    }
    public static void ThrowIfTrue(bool value, string message)
    {
        if (value)
        {
            throw new ApiException(HttpResponseStatusType.ValidationError, message);
        }
    }


    public static void ThrowIfFalse(bool value, HttpResponseStatusType httpResponseStatusType)
    {
        if (value is false)
        {
            throw new ApiException(httpResponseStatusType);
        }
    }



    public static void ThrowIfFalse(bool value, string message)
    {
        if (value is false)
        {
            throw new ApiException(HttpResponseStatusType.ValidationError, message);
        }
    }
}
