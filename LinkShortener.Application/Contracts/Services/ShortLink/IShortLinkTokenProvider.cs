namespace ch4rniauski.LinkShortener.Application.Contracts.Services.ShortLink;

public interface IShortLinkTokenProvider
{
    string GenerateToken(int length = 7);
}
