using ch4rniauski.LinkShortener.Application.Common.Results;
using ch4rniauski.LinkShortener.Application.Dto.GoogleOAuth.Responses;
using ch4rniauski.LinkShortener.Application.Models.Google;
using MediatR;

namespace ch4rniauski.LinkShortener.Application.UseCases.Commands.GoogleOAuth;

public sealed record GetUserInfoCommand(GoogleOAuthResponse GoogleOAuthResponse) : IRequest<Result<TokenValidationResultDto>>;
