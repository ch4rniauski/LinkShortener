namespace ch4rniauski.LinkShortener.Application.Dto.GoogleOAuth.Responses;

public sealed record TokenValidationResultDto(
    string Email,
    string Picture,
    string Name);
