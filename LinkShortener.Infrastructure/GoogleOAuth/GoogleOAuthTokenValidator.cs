using ch4rniauski.LinkShortener.Application.Contracts.Authentication;
using ch4rniauski.LinkShortener.Application.Dto.GoogleOAuth.Responses;
using Google.Apis.Auth;

namespace ch4rniauski.LinkShortener.Infrastructure.GoogleOAuth;

internal sealed class GoogleOAuthTokenValidator : ITokenValidator
{
    public async Task<TokenValidationResultDto?> ValidateAsync(string token)
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(token);
            
            return new TokenValidationResultDto(payload.Email, payload.Picture, payload.Name);
        }
        catch
        {
            return null;
        }
    }
}
