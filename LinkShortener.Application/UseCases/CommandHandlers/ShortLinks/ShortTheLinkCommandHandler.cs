using ch4rniauski.LinkShortener.Application.Common.Results;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using ch4rniauski.LinkShortener.Application.UseCases.Commands.ShortLinks;
using MediatR;

namespace ch4rniauski.LinkShortener.Application.UseCases.CommandHandlers.ShortLinks;

internal sealed class ShortTheLinkCommandHandler : IRequestHandler<ShortTheLinkCommand, Result<ShortTheLinkResponseDto>>
{
    public async Task<Result<ShortTheLinkResponseDto>> Handle(ShortTheLinkCommand request, CancellationToken cancellationToken)
    {
        
    }
}