using ch4rniauski.LinkShortener.Application.Common.Errors;
using ch4rniauski.LinkShortener.Application.Common.Results;
using ch4rniauski.LinkShortener.Application.Contracts.Repositories;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using ch4rniauski.LinkShortener.Application.UseCases.Queries.ShortLink;
using MediatR;

namespace ch4rniauski.LinkShortener.Application.UseCases.QueryHandlers.ShortLink;

internal sealed class GetOriginalUrlByShortTokenQueryHandler : IRequestHandler<GetOriginalUrlByShortTokenQuery, Result<RedirectByShortLinkResponse>>
{
    private readonly IShortLinkRepository _repository;

    public GetOriginalUrlByShortTokenQueryHandler(IShortLinkRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<RedirectByShortLinkResponse>> Handle(GetOriginalUrlByShortTokenQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByTokenAsync(request.Token, cancellationToken);

        if (entity is null)
        {
            return Result<RedirectByShortLinkResponse>
                .Failure(Error.NotFound($"Url with token {request.Token} does not exist"));
        }

        var response = new RedirectByShortLinkResponse(entity.OriginalUrl);
        
        return Result<RedirectByShortLinkResponse>.Success(response);
    }
}
