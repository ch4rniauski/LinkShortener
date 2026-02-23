using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using ch4rniauski.LinkShortener.Domain.Entities;

namespace ch4rniauski.LinkShortener.Application.Contracts.Repositories;

public interface IShortLinkRepository
{
    Task<bool> AddAsync(ShortLinkEntity entity, CancellationToken cancellationToken = default);
    Task<ShortLinkEntity?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<ShortLinkEntity?> GetByOriginalUrlAsync(string originalUrl, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(ShortLinkEntity entity, CancellationToken cancellationToken = default);
    Task<IList<TMap>> GetShortLinksByPage<TMap>(int page = 1, int pageSize = 15, CancellationToken cancellationToken = default);
}
