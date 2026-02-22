using ch4rniauski.LinkShortener.Application.Contracts.Services.ShortLink;
using ch4rniauski.LinkShortener.Application.Services.ShortLink;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ch4rniauski.LinkShortener.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(_ => {}, typeof(ServiceCollectionExtensions).Assembly);
    }

    public static void AddMediatrConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        });
    }

    public static void AddValidationConfiguration(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
    }

    public static void AddTokenProvidersConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IShortLinkTokenProvider, ShortLinkTokenProvider>();
    }
}
