using ch4rniauski.LinkShortener.Application.Common.Results;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Requests;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using MediatR;

namespace ch4rniauski.LinkShortener.Application.UseCases.Commands.ShortLink;

public sealed record UpdateLongLinkCommand(UpdateLongLinkRequestDto Request, Guid Id) : IRequest<Result<UpdateLongLinkResponseDto>>;
