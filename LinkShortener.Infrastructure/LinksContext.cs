using LinkShortener.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Infrastructure;

public class LinksContext : DbContext
{
    public DbSet<ShortLinkEntity> ShortLinks { get; set; }

    public LinksContext(DbContextOptions<LinksContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(LinksContext).Assembly);
}
