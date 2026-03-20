using ch4rniauski.LinkShortener.Application.Contracts.Authentication;
using ch4rniauski.LinkShortener.Application.Contracts.Repositories;
using ch4rniauski.LinkShortener.Infrastructure.GoogleOAuth;
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
                serverVersion: ServerVersion.AutoDetect(configuration.GetConnectionString("MariaDbLinks"))
                )
            );

        services.AddScoped<IShortLinkRepository, ShortLinkRepository>();
    }

    public static void AddTokenConfiguration(this IServiceCollection services)
    {
        services.AddSingleton<ITokenValidator, GoogleOAuthTokenValidator>();
    }
}
