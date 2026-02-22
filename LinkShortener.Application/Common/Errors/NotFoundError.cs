namespace ch4rniauski.LinkShortener.Application.Common.Errors;

public class NotFoundError : Error
{
    private const int NotFoundStatusCode = 404;
    
    public NotFoundError(string message) : base(NotFoundStatusCode, message)
    {
    }
}
