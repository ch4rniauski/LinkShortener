using ch4rniauski.LinkShortener.Application.Common.Errors;
using ch4rniauski.LinkShortener.Application.Common.Results;
using ch4rniauski.LinkShortener.Application.Contracts.Authentication;
using ch4rniauski.LinkShortener.Application.Dto.GoogleOAuth.Responses;
using ch4rniauski.LinkShortener.Application.UseCases.Commands.GoogleOAuth;
using MediatR;

namespace ch4rniauski.LinkShortener.Application.UseCases.CommandHandlers.GoogleOAuth;

internal sealed class GetUserInfoCommandHandler : IRequestHandler<GetUserInfoCommand, Result<TokenValidationResultDto>>
{
    private readonly ITokenValidator _tokenValidator;

    public GetUserInfoCommandHandler(ITokenValidator tokenValidator)
    {
        _tokenValidator = tokenValidator;
    }

    public async Task<Result<TokenValidationResultDto>> Handle(GetUserInfoCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _tokenValidator.ValidateAsync(request.GoogleOAuthResponse.IdToken);

        if (validationResult is null)
        {
            return Result<TokenValidationResultDto>.Failure(
                Error.FailedValidation(
                    "Exception was thrown while validating token"
                    ));
        }
        
        return Result<TokenValidationResultDto>.Success(validationResult);
    }
}
