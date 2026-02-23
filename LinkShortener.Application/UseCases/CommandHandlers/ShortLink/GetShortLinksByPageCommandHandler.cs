using ch4rniauski.LinkShortener.Application.Contracts.Repositories;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using ch4rniauski.LinkShortener.Application.UseCases.Commands.ShortLink;
using MediatR;

namespace ch4rniauski.LinkShortener.Application.UseCases.CommandHandlers.ShortLink;

internal sealed class GetShortLinksByPageCommandHandler : IRequestHandler<GetShortLinksByPageCommand, IList<GetShortLinkResponseDto>>
{
    private readonly IShortLinkRepository _repository;

    public GetShortLinksByPageCommandHandler(IShortLinkRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<GetShortLinkResponseDto>> Handle(GetShortLinksByPageCommand request, CancellationToken cancellationToken)
        => await _repository.GetShortLinksByPageAsync<GetShortLinkResponseDto>(request.Page, request.PageSize, cancellationToken);
}
