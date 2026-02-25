namespace LinkShortener.Tests.Helpers.UrlProvider;

internal static class ShortLinksUrlProvider
{
    private const string BaseUri = "links";

    public static string GetDeleteShortLinkUrl(Guid id)
        => $"{BaseUri}/{id}";
}
