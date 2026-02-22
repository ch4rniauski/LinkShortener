using ch4rniauski.LinkShortener.Application.Common.Errors;

namespace ch4rniauski.LinkShortener.Application.Common.Results;

public class Result<T>
{
    public bool IsSuccess { get; }

    public T? Value { get; }

    public Error? Error { get; }

    private Result(
        bool isSuccess,
        T? value,
        Error? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }
    
    public static Result<T> Success(T value)
        => new Result<T>(
            isSuccess: true,
            value: value,
            error: null);
    
    public static Result<T> Failure(Error error)
        => new Result<T>(
            isSuccess: false,
            value: default,
            error: error);
}
