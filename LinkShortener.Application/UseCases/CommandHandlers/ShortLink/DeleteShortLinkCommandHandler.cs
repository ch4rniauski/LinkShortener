using ch4rniauski.LinkShortener.Application.Common.Errors;
using ch4rniauski.LinkShortener.Application.Common.Results;
using ch4rniauski.LinkShortener.Application.Contracts.Repositories;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using ch4rniauski.LinkShortener.Application.UseCases.Commands.ShortLink;
using MediatR;

namespace ch4rniauski.LinkShortener.Application.UseCases.CommandHandlers.ShortLink;

internal sealed class DeleteShortLinkCommandHandler : IRequestHandler<DeleteShortLinkCommand, Result<DeleteShortLinkResponseDto>>
{
    private readonly IShortLinkRepository _repository;

    public DeleteShortLinkCommandHandler(IShortLinkRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<DeleteShortLinkResponseDto>> Handle(DeleteShortLinkCommand request, CancellationToken cancellationToken)
    {
        var isDeleted = await _repository.DeleteAsync(request.Id, cancellationToken);

        if (!isDeleted)
        {
            return Result<DeleteShortLinkResponseDto>.Failure(
                Error.NotFound($"Short link with id {request.Id} was not found.")
            );
        }

        var response = new DeleteShortLinkResponseDto(request.Id);
        
        return Result<DeleteShortLinkResponseDto>.Success(response);
    }
}
