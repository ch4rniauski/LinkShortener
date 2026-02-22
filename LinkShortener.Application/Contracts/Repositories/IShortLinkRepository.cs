using ch4rniauski.LinkShortener.Domain.Entities;

namespace ch4rniauski.LinkShortener.Application.Contracts.Repositories;

public interface IShortLinkRepository
{
    Task<bool> AddAsync(ShortLinkEntity entity, CancellationToken cancellationToken = default);
    Task<ShortLinkEntity?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
}
