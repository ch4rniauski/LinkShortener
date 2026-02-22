using ch4rniauski.LinkShortener.Application.Common.Results;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Requests;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using MediatR;

namespace ch4rniauski.LinkShortener.Application.UseCases.Commands.ShortLink;

public sealed record ShortTheLinkCommand(ShortTheLinkRequestDto Request) : IRequest<Result<ShortTheLinkResponseDto>>;
