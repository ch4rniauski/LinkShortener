using AutoMapper;
using ch4rniauski.LinkShortener.Application.Common.Errors;
using ch4rniauski.LinkShortener.Application.Common.Results;
using ch4rniauski.LinkShortener.Application.Contracts.Repositories;
using ch4rniauski.LinkShortener.Application.Contracts.Services.ShortLink;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Requests;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using ch4rniauski.LinkShortener.Application.UseCases.Commands.ShortLink;
using ch4rniauski.LinkShortener.Domain.Entities;
using FluentValidation;
using MediatR;

namespace ch4rniauski.LinkShortener.Application.UseCases.CommandHandlers.ShortLink;

internal sealed class ShortTheLinkCommandHandler : IRequestHandler<ShortTheLinkCommand, Result<ShortTheLinkResponseDto>>
{
    private readonly IShortLinkRepository _repository;
    private readonly IValidator<ShortTheLinkRequestDto> _validator;
    private readonly IMapper _mapper;
    private readonly IShortLinkTokenProvider _tokenProvider;
    
    private const string BaseUrl = "http://localhost:8080/";

    public ShortTheLinkCommandHandler(
        IShortLinkRepository repository,
        IValidator<ShortTheLinkRequestDto> validator,
        IMapper mapper,
        IShortLinkTokenProvider tokenProvider)
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<ShortTheLinkResponseDto>> Handle(ShortTheLinkCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var message = string.Join("", validationResult.Errors);
            
            return Result<ShortTheLinkResponseDto>
                .Failure(Error.FailedValidation(message));
        }
        
        var entity = _mapper.Map<ShortLinkEntity>(request.Request);

        string token;
        while (true)
        {
            token = _tokenProvider.GenerateToken();
            
            var shortLink = await _repository.GetByTokenAsync(token, cancellationToken);

            if (shortLink is null)
            {
                break;
            }
        }

        entity.ShortToken = token;
        
        var isAdded = await _repository.AddAsync(entity, cancellationToken);

        if (!isAdded)
        {
            return Result<ShortTheLinkResponseDto>
                .Failure(Error.InternalError("Error occured while adding short link"));
        }

        var response = new ShortTheLinkResponseDto(BaseUrl + token);
        
        return Result<ShortTheLinkResponseDto>.Success(response);
    }
}