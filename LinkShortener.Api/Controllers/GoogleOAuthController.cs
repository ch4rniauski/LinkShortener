using ch4rniauski.LinkShortener.Application.Dto.GoogleOAuth.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace ch4rniauski.LinkShortener.Api.Controllers;

[ApiController]
[Route("google")]
public class GoogleOAuthController : ControllerBase
{
    [HttpGet("redirect")]
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

    [HttpPost("callback")]
    public async Task<ActionResult> Callback(
        [FromBody] GoogleOAuthCallbackRequestDto request,
        CancellationToken cancellationToken)
    {
        const string googleOAuthBaseUrl = "https://oauth2.googleapis.com/token";
        
        using var client = new HttpClient();
        
        var data = new Dictionary<string, string>
        {
            ["client_id"] = Environment.GetEnvironmentVariable("OAUTH_GOOGLE_CLIENT_ID") ?? string.Empty,
            ["client_secret"] = Environment.GetEnvironmentVariable("OAUTH_GOOGLE_CLIENT_SECRET") ?? string.Empty,
            ["code"] = request.Code,
            ["grant_type"] = "authorization_code",
            ["redirect_uri"] = "http://localhost:4200/auth/google"
        };

        var response = await client.PostAsync(googleOAuthBaseUrl, new FormUrlEncodedContent(data), cancellationToken);
        
        var json = await response.Content.ReadAsStringAsync(cancellationToken);

        return Ok(json);
    }
}
