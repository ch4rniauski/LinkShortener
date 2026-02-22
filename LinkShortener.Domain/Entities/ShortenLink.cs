namespace LinkShortener.Domain.Entities;

public class ShortenLink
{
    public Guid Id { get; set; }

    public string Token { get; set; } = null!;

    public string FullUrl { get; set; } = null!;
    
    public int ClickCount { get; set; }
}
