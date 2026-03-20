using ch4rniauski.LinkShortener.Application.Dto.GoogleOAuth.Responses;

namespace ch4rniauski.LinkShortener.Application.Contracts.Authentication;

public interface ITokenValidator
{
    Task<TokenValidationResultDto?> ValidateAsync(string token);
}
