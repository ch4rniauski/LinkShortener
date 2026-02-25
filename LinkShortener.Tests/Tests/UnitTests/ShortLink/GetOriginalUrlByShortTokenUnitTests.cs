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

public sealed class GetOriginalUrlByShortTokenUnitTests
{
    private readonly GetOriginalUrlByShortTokenCommandHandler _commandHandler;
    private readonly Mock<IShortLinkRepository> _mockRepository = new();

    public GetOriginalUrlByShortTokenUnitTests()
    {
        _commandHandler = new GetOriginalUrlByShortTokenCommandHandler(_mockRepository.Object);
    }
    
    [Fact]
    private async Task GetOriginalUrlByShortToken_ReturnsSuccessfulResult()
    {
        // Arrange
        const bool isSuccess = true;
        
        const string shortToken = "shortToken";
        const string originalUrl = "originalUrl";
        var command = new GetOriginalUrlByShortTokenCommand(shortToken);
        var entity = new ShortLinkEntity
        {
            OriginalUrl = originalUrl
        };
        var responseDto = new RedirectByShortLinkResponse(entity.OriginalUrl);
        
        _mockRepository.Setup(r => r.GetByTokenAsync(
                shortToken, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);

        _mockRepository.Setup(r => r.UpdateAsync(
                entity,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(isSuccess);

        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Result<RedirectByShortLinkResponse>>(response);
        Assert.NotNull(response.Value);
        Assert.IsType<RedirectByShortLinkResponse>(response.Value);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.Equal(responseDto, response.Value);
        Assert.Null(response.Error);
    }
    
    [Fact]
    private async Task GetOriginalUrlByShortToken_ReturnsFailedResult_WithNotFoundError()
    {
        // Arrange
        const bool isSuccess = false;
        
        const string shortToken = "shortToken";
        var command = new GetOriginalUrlByShortTokenCommand(shortToken);
        ShortLinkEntity? entity = null;
        
        _mockRepository.Setup(r => r.GetByTokenAsync(
                shortToken, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);

        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Result<RedirectByShortLinkResponse>>(response);
        Assert.Null(response.Value);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.IsType<NotFoundError>(response.Error);
    }
}
