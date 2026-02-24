using AutoMapper;
using AutoMapper.QueryableExtensions;
using ch4rniauski.LinkShortener.Application.Contracts.Repositories;
using ch4rniauski.LinkShortener.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ch4rniauski.LinkShortener.Infrastructure.Repositories;

internal sealed class ShortLinkRepository : IShortLinkRepository
{
    private readonly LinksContext _context;
    private readonly DbSet<ShortLinkEntity> _dbSet;
    private readonly IMapper _mapper;

    public ShortLinkRepository(
        LinksContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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

    public async Task<ShortLinkEntity?> GetByOriginalUrlAsync(string originalUrl, CancellationToken cancellationToken = default)
        => await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.OriginalUrl == originalUrl, cancellationToken);

    public async Task<bool> UpdateAsync(ShortLinkEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FindAsync(keyValues: [id], cancellationToken: cancellationToken);

        if (entity is null)
        {
            return false;
        }
        
        _dbSet.Remove(entity);
        
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<IList<TMap>> GetShortLinksByPageAsync<TMap>(int page = 1, int pageSize = 15,
        CancellationToken cancellationToken = default)
        => await _dbSet
            .AsNoTracking()
            .OrderByDescending(s => s.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ProjectTo<TMap>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
}
