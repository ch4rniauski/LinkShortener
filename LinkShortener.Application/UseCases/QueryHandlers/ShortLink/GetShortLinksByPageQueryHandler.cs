using ch4rniauski.LinkShortener.Application.Contracts.Repositories;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using ch4rniauski.LinkShortener.Application.UseCases.Queries.ShortLink;
using MediatR;

namespace ch4rniauski.LinkShortener.Application.UseCases.QueryHandlers.ShortLink;

internal sealed class GetShortLinksByPageQueryHandler : IRequestHandler<GetShortLinksByPageQuery, GetShortLinksByPageResponseDto>
{
    private readonly IShortLinkRepository _repository;

    public GetShortLinksByPageQueryHandler(IShortLinkRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetShortLinksByPageResponseDto> Handle(GetShortLinksByPageQuery request, CancellationToken cancellationToken)
    {
        var links = await _repository.GetShortLinksByPageAsync<GetShortLinkResponseDto>(request.Page, request.PageSize, cancellationToken);
        
        var totalLinksCount = await _repository.GetTotalLinksCountAsync(cancellationToken);
        
        return new GetShortLinksByPageResponseDto(links, totalLinksCount);
    }
}
