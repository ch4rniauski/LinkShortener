namespace ch4rniauski.LinkShortener.Application.Common.Errors;

public abstract class Error
{
    public int StatusCode { get; set; }

    public string Description { get; set; }

    protected Error(int statusCode, string description)
    {
        StatusCode = statusCode;
        Description = description;
    }
}
