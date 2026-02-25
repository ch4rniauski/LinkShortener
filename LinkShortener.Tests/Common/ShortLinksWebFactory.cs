using ch4rniauski.LinkShortener.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MariaDb;
using Xunit;

namespace LinkShortener.Tests.Common;

public sealed class ShortLinksWebFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MariaDbContainer _dbContainer = new MariaDbBuilder("mariadb:lts-ubi9")
        .WithDatabase("Links")
        .WithUsername("example-user")
        .WithPassword("5432")
        .WithEnvironment("MARIADB_ROOT_PASSWORD", "5432")
        .Build();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<LinksContext>>();

            services.AddDbContext<LinksContext>(opt =>
            {
                opt.UseMySql(
                    connectionString: _dbContainer.GetConnectionString(),
                    serverVersion: ServerVersion.AutoDetect(_dbContainer.GetConnectionString())
                );
            });
        });
    }

    public async Task InitializeAsync()
        => await _dbContainer.StartAsync();

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();
    }
}
