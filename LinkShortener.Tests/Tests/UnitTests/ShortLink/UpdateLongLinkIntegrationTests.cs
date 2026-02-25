using ch4rniauski.LinkShortener.Application.Common.Errors;
using ch4rniauski.LinkShortener.Application.Common.Results;
using ch4rniauski.LinkShortener.Application.Contracts.Repositories;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Requests;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using ch4rniauski.LinkShortener.Application.UseCases.CommandHandlers.ShortLink;
using ch4rniauski.LinkShortener.Application.UseCases.Commands.ShortLink;
using ch4rniauski.LinkShortener.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace LinkShortener.Tests.Tests.UnitTests.ShortLink;

public sealed class UpdateLongLinkIntegrationTests
{
    private readonly UpdateLongLinkCommandHandler _commandHandler;
    private readonly Mock<IShortLinkRepository> _mockRepository = new();
    private readonly Mock<IValidator<UpdateLongLinkRequestDto>> _mockValidator = new();

    public UpdateLongLinkIntegrationTests()
    {
        _commandHandler = new UpdateLongLinkCommandHandler(
            _mockRepository.Object,
            _mockValidator.Object);
    }

    [Fact]
    private async Task UpdateLongLink_ReturnsSuccessfulResult()
    {
        // Arrange
        const bool isSuccess = true;
        const string newLongLink = "newLongLink";
        
        var id = Guid.NewGuid();
        var request = new UpdateLongLinkRequestDto(newLongLink);
        var command = new UpdateLongLinkCommand(request, id);
        var entity = new ShortLinkEntity();
        
        _mockValidator.Setup(v => v.ValidateAsync(
                request,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        _mockRepository.Setup(r => r.GetByIdAsync(
                command.Id, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);

        _mockRepository.Setup(r => r.UpdateAsync(
                entity,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Result<UpdateLongLinkResponseDto>>(response);
        Assert.NotNull(response.Value);
        Assert.IsType<UpdateLongLinkResponseDto>(response.Value);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.Null(response.Error);
    }
    
    [Fact]
    private async Task UpdateLongLink_ReturnsFailedResult_WithValidationError()
    {
        // Arrange
        const bool isSuccess = false;
        const string newLongLink = "newLongLink";
        
        var id = Guid.NewGuid();
        var request = new UpdateLongLinkRequestDto(newLongLink);
        var command = new UpdateLongLinkCommand(request, id);
        
        _mockValidator.Setup(v => v.ValidateAsync(
                request,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult
            {
                Errors =
                [
                    new ValidationFailure()
                ]
            });

        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Result<UpdateLongLinkResponseDto>>(response);
        Assert.IsType<ValidationError>(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Null(response.Value);
    }
    
    [Fact]
    private async Task UpdateLongLink_ReturnsFailedResult_WithNotFoundError()
    {
        // Arrange
        const bool isSuccess = false;
        const string newLongLink = "newLongLink";
        
        var id = Guid.NewGuid();
        var request = new UpdateLongLinkRequestDto(newLongLink);
        var command = new UpdateLongLinkCommand(request, id);
        ShortLinkEntity? entity = null;
        
        _mockValidator.Setup(v => v.ValidateAsync(
                request,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        _mockRepository.Setup(r => r.GetByIdAsync(
                command.Id, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);
        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Result<UpdateLongLinkResponseDto>>(response);
        Assert.IsType<NotFoundError>(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Null(response.Value);
    }
    
    [Fact]
    private async Task UpdateLongLink_ReturnsFailedResult_WithInternalError()
    {
        // Arrange
        const bool isSuccess = false;
        const string newLongLink = "newLongLink";
        
        var id = Guid.NewGuid();
        var request = new UpdateLongLinkRequestDto(newLongLink);
        var command = new UpdateLongLinkCommand(request, id);
        var entity = new ShortLinkEntity();
        
        _mockValidator.Setup(v => v.ValidateAsync(
                request,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        _mockRepository.Setup(r => r.GetByIdAsync(
                command.Id, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);

        _mockRepository.Setup(r => r.UpdateAsync(
                entity,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Result<UpdateLongLinkResponseDto>>(response);
        Assert.IsType<InternalServerError>(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Null(response.Value);
    }
}
