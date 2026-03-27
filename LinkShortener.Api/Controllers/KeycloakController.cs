using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace ch4rniauski.LinkShortener.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class KeycloakController : ControllerBase
{
    [HttpGet("redirect")]
    public ActionResult LoginRedirect()
    {
        const string keycloakBaseUrl = "http://localhost:8081/realms/linkshortener/protocol/openid-connect/auth";

        var queryParams = new Dictionary<string, string?>
        {
            ["client_id"] = Environment.GetEnvironmentVariable("OAUTH_GOOGLE_CLIENT_ID") ?? string.Empty,
            ["redirect_uri"] = "http://localhost:4200/auth/callback",
            ["response_type"] = "code",
            ["scope"] = "openid"
        };
        
        var redirectUrl = QueryHelpers.AddQueryString(keycloakBaseUrl, queryParams);
        
        return Redirect(redirectUrl);
    }
}
