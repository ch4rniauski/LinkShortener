using ch4rniauski.LinkShortener.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ch4rniauski.LinkShortener.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task ApplyMigrationsAsync(this IApplicationBuilder app)
    {
        await using var scope = app.ApplicationServices.CreateAsyncScope();

        await using var db = scope.ServiceProvider.GetRequiredService<LinksContext>();

        await db.Database.MigrateAsync();
    }
}
