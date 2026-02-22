using ch4rniauski.LinkShortener.Application.Common.Results;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using MediatR;

namespace ch4rniauski.LinkShortener.Application.UseCases.Commands.ShortLink;

public sealed record GetOriginalUrlByShortTokenCommand(string Token) : IRequest<Result<RedirectByShortLinkResponse>>;
