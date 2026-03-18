using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace ch4rniauski.LinkShortener.Api.Controllers;

[ApiController]
[Route("auth/google")]
public class OAuthController : ControllerBase
{
    [HttpGet]
    public ActionResult RedirectToGoogleOAuth()
    {
        const string googleOAuthBaseUrl = "https://accounts.google.com/o/oauth2/v2/auth";
        var queryParams = new Dictionary<string, string?>
        {
            ["client_id"] = Environment.GetEnvironmentVariable("OAUTH_GOOGLE_CLIENT_ID") ?? string.Empty,
            ["redirect_uri"] = "http://localhost:4200/auth/google",
            ["response_type"] = "code",
            ["scope"] = "email openid profile"
        };
        
        var redirectStr = QueryHelpers.AddQueryString(googleOAuthBaseUrl, queryParams);
        
        return Redirect(redirectStr);
    }
}
