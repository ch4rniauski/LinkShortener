using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using MediatR;

namespace ch4rniauski.LinkShortener.Application.UseCases.Commands.ShortLink;

public sealed record GetShortLinksByPageCommand(int Page, int PageSize) : IRequest<IList<GetShortLinkResponseDto>>;
