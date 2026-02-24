using ch4rniauski.LinkShortener.Application.Common.Errors;
using ch4rniauski.LinkShortener.Application.Common.Results;
using ch4rniauski.LinkShortener.Application.Contracts.Repositories;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Requests;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using ch4rniauski.LinkShortener.Application.UseCases.Commands.ShortLink;
using FluentValidation;
using MediatR;

namespace ch4rniauski.LinkShortener.Application.UseCases.CommandHandlers.ShortLink;

internal sealed class UpdateLongLinkCommandHandler : IRequestHandler<UpdateLongLinkCommand, Result<UpdateLongLinkResponseDto>>
{
    private readonly IShortLinkRepository _repository;
    private readonly IValidator<UpdateLongLinkRequestDto> _validator;

    public UpdateLongLinkCommandHandler(
        IShortLinkRepository repository,
        IValidator<UpdateLongLinkRequestDto> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<UpdateLongLinkResponseDto>> Handle(UpdateLongLinkCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var message = string.Join("", validationResult.Errors);
            
            return Result<UpdateLongLinkResponseDto>
                .Failure(Error.FailedValidation(message));
        }
        
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<UpdateLongLinkResponseDto>
                .Failure(Error.NotFound(
                    $"Link with id {request.Id} was not found."
                    ));
        }

        entity.OriginalUrl = request.Request.NewLongLink;
        
        var isUpdated = await _repository.UpdateAsync(entity, cancellationToken);

        if (!isUpdated)
        {
            return Result<UpdateLongLinkResponseDto>
                .Failure(Error.InternalError(
                    $"Short link with id {request.Id} was not updated."
                ));
        }

        var response = new UpdateLongLinkResponseDto(request.Request.NewLongLink);
        
        return Result<UpdateLongLinkResponseDto>.Success(response);
    }
}
