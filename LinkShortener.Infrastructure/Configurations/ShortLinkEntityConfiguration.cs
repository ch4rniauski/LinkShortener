using LinkShortener.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkShortener.Infrastructure.Configurations;

internal sealed class ShortLinkEntityConfiguration : IEntityTypeConfiguration<ShortLinkEntity>
{
    public void Configure(EntityTypeBuilder<ShortLinkEntity> builder)
    {
        builder.HasKey(s => s.Id);

        builder
            .Property(s => s.ShortToken)
            .IsRequired()
            .HasMaxLength(7);

        builder
            .HasIndex(s => s.ShortToken)
            .IsUnique();
        
        builder
            .Property(s => s.OriginalUrl)
            .IsRequired()
            .HasMaxLength(2048);

        builder
            .Property(s => s.ClickCount)
            .IsRequired()
            .HasDefaultValue(0);
        
        builder.
            Property(s => s.CreatedAt)
            .IsRequired();
    }
}
