using ch4rniauski.LinkShortener.Application.Common.Errors;
using ch4rniauski.LinkShortener.Application.Common.Results;
using ch4rniauski.LinkShortener.Application.Contracts.Repositories;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using ch4rniauski.LinkShortener.Application.UseCases.CommandHandlers.ShortLink;
using ch4rniauski.LinkShortener.Application.UseCases.Commands.ShortLink;
using ch4rniauski.LinkShortener.Domain.Entities;
using Moq;
using Xunit;

namespace LinkShortener.Tests.Tests.UnitTests.ShortLink;

public sealed class DeleteShortLinkUnitTests
{
    private readonly DeleteShortLinkCommandHandler _commandHandler;
    private readonly Mock<IShortLinkRepository> _mockRepository = new();

    public DeleteShortLinkUnitTests()
    {
        _commandHandler = new DeleteShortLinkCommandHandler(_mockRepository.Object);
    }

    [Fact]
    private async Task DeleteShortLink_ReturnsSuccessfulResult()
    {
        // Arrange
        const bool isSuccess = true;
        
        var id = Guid.NewGuid();
        var command = new DeleteShortLinkCommand(id);
        var entity = new ShortLinkEntity();
        var responseDto = new DeleteShortLinkResponseDto(id);
        
        _mockRepository.Setup(r => r.GetByIdAsync(
                id, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);

        _mockRepository.Setup(r => r.DeleteAsync(
                entity,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(isSuccess);

        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Result<DeleteShortLinkResponseDto>>(response);
        Assert.NotNull(response.Value);
        Assert.IsType<DeleteShortLinkResponseDto>(response.Value);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.Equal(responseDto, response.Value);
        Assert.Null(response.Error);
    }
    
    [Fact]
    private async Task DeleteShortLink_ReturnsFailedResult_WithNotFoundError()
    {
        // Arrange
        const bool isSuccess = false;
        
        var id = Guid.NewGuid();
        var command = new DeleteShortLinkCommand(id);
        ShortLinkEntity? entity = null;
        
        _mockRepository.Setup(r => r.GetByIdAsync(
                id, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);

        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Result<DeleteShortLinkResponseDto>>(response);
        Assert.Null(response.Value);
        Assert.NotNull(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.IsType<NotFoundError>(response.Error);
    }
    
    [Fact]
    private async Task DeleteShortLink_ReturnsFailedResult_WithInternalError()
    {
        // Arrange
        const bool isSuccess = false;
        
        var id = Guid.NewGuid();
        var command = new DeleteShortLinkCommand(id);
        var entity = new ShortLinkEntity();
        
        _mockRepository.Setup(r => r.GetByIdAsync(
                id, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);

        _mockRepository.Setup(r => r.DeleteAsync(
                entity,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(isSuccess);

        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Result<DeleteShortLinkResponseDto>>(response);
        Assert.Null(response.Value);
        Assert.NotNull(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.IsType<InternalServerError>(response.Error);
    }
}
