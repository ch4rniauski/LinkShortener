using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using MediatR;

namespace ch4rniauski.LinkShortener.Application.UseCases.Queries.ShortLink;

public sealed record GetShortLinksByPageQuery(int Page, int PageSize) : IRequest<GetShortLinksByPageResponseDto>;
