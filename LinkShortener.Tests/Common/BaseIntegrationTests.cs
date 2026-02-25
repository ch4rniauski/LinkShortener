using ch4rniauski.LinkShortener.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LinkShortener.Tests.Common;

public abstract class BaseIntegrationTests : IClassFixture<ShortLinksWebFactory>
{
    protected readonly LinksContext DbContext;
    protected readonly HttpClient HttpClient;
    
    protected BaseIntegrationTests(ShortLinksWebFactory factory)
    {
        var serviceScope = factory.Services.CreateScope();

        DbContext = serviceScope.ServiceProvider.GetRequiredService<LinksContext>();
        
        HttpClient = factory.CreateClient();
    }
}
