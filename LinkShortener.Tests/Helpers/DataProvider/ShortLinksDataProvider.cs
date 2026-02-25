using Bogus;
using ch4rniauski.LinkShortener.Domain.Entities;

namespace LinkShortener.Tests.Helpers.DataProvider;

internal static class ShortLinksDataProvider
{
    public static ShortLinkEntity GenerateShortLinkEntity()
    {
        return new Faker<ShortLinkEntity>()
            .RuleFor(c => c.Id, _ => Guid.NewGuid())
            .RuleFor(c => c.ClickCount, _ => 0)
            .RuleFor(c => c.CreatedAt, faker => faker.Date.Past())
            .RuleFor(c => c.OriginalUrl, faker => faker.Internet.Url())
            .RuleFor(c => c.ShortToken, _ => "shortToken")
            .Generate();
    }
}
