using ch4rniauski.LinkShortener.Application.Contracts.Repositories;
using ch4rniauski.LinkShortener.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ch4rniauski.LinkShortener.Infrastructure.Repositories;

internal sealed class ShortLinkRepository : IShortLinkRepository
{
    private readonly LinksContext _context;
    private readonly DbSet<ShortLinkEntity> _dbSet;

    public ShortLinkRepository(LinksContext context)
    {
        _context = context;
        _dbSet = _context.ShortLinks;
    }

    public async Task<bool> AddAsync(ShortLinkEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<ShortLinkEntity?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        => await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.ShortToken == token, cancellationToken);
}