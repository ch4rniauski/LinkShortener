using ch4rniauski.LinkShortener.Application.Common.Results;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using MediatR;

namespace ch4rniauski.LinkShortener.Application.UseCases.Queries.ShortLink;

public sealed record GetOriginalUrlByShortTokenQuery(string Token) : IRequest<Result<RedirectByShortLinkResponse>>;
