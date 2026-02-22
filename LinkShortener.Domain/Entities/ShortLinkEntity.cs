namespace LinkShortener.Domain.Entities;

public class ShortLinkEntity
{
    public Guid Id { get; set; }

    public string ShortToken { get; set; } = null!;

    public string OriginalUrl { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    
    public int ClickCount { get; set; }
}
