using System.Net;
using LinkShortener.Tests.Common;
using LinkShortener.Tests.Helpers.DataProvider;
using LinkShortener.Tests.Helpers.UrlProvider;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LinkShortener.Tests.Tests.IntegrationTests.ShortLink;

public sealed class DeleteShortLinkIntegrationTests : BaseIntegrationTests
{
    public DeleteShortLinkIntegrationTests(ShortLinksWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task DeleteShortLink_ReturnsOk()
    {
        // Arrange
        var entity = ShortLinksDataProvider.GenerateShortLinkEntity();
        var url = ShortLinksUrlProvider.GetDeleteShortLinkUrl(entity.Id);
        
        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        await DbContext.ShortLinks.AddAsync(entity);
        await DbContext.SaveChangesAsync();
        
        var response = await HttpClient.DeleteAsync(url);

        var client = await DbContext.ShortLinks
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == entity.Id);

        // // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Null(client);
    }
}
