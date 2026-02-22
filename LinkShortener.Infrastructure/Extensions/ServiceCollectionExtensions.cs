using ch4rniauski.LinkShortener.Application.Contracts.Repositories;
using ch4rniauski.LinkShortener.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ch4rniauski.LinkShortener.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddLinksContextConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LinksContext>(opt => 
            opt.UseMySql(
                connectionString: configuration.GetConnectionString("MariaDbLinks"),
                serverVersion: ServerVersion.AutoDetect(configuration.GetConnectionString("MariaDbLinks")))
            );

        services.AddScoped<IShortLinkRepository, ShortLinkRepository>();
    }
}
