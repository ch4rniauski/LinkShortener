namespace ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;

public sealed record GetShortLinksByPageResponseDto(
    IList<GetShortLinkResponseDto> Links,
    int TotalLinksCount);
