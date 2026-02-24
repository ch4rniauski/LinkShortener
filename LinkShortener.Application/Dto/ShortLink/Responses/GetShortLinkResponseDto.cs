namespace ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;

public sealed record GetShortLinkResponseDto(
    Guid Id,
    string ShortToken,
    string OriginalUrl,
    DateTime CreatedAt,
    int ClickCount);
