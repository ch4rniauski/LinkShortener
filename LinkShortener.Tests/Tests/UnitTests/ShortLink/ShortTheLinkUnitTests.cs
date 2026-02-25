using AutoMapper;
using ch4rniauski.LinkShortener.Application.Common.Errors;
using ch4rniauski.LinkShortener.Application.Common.Results;
using ch4rniauski.LinkShortener.Application.Contracts.Repositories;
using ch4rniauski.LinkShortener.Application.Contracts.Services.ShortLink;
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

public sealed class ShortTheLinkUnitTests
{
    private readonly ShortTheLinkCommandHandler _commandHandler;
    private readonly Mock<IShortLinkRepository> _mockRepository = new();
    private readonly Mock<IValidator<ShortTheLinkRequestDto>> _mockValidator = new();
    private readonly Mock<IMapper> _mockMapper = new();
    private readonly Mock<IShortLinkTokenProvider> _mockShortLinkProvider = new();

    public ShortTheLinkUnitTests()
    {
        _commandHandler = new ShortTheLinkCommandHandler(
            _mockRepository.Object,
            _mockValidator.Object,
            _mockMapper.Object,
            _mockShortLinkProvider.Object);
    }

    [Fact]
    private async Task ShortTheLink_ReturnsSuccessfulResult()
    {
        // Arrange
        const bool isSuccess = true;
        const int tokenLength = 7;
        const string originalUrl = "originalUrl";
        const string baseUrl = "baseUrl";
        const string shortToken = "shortToken";
        
        var request = new ShortTheLinkRequestDto(originalUrl);
        var command = new ShortTheLinkCommand(request, baseUrl);
        var entity = new ShortLinkEntity();
        ShortLinkEntity? entityByOriginalUrl = null;
        ShortLinkEntity? entityByShortToken = null;
        
        _mockValidator.Setup(v => v.ValidateAsync(
                request,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        _mockRepository.Setup(r => r.GetByOriginalUrlAsync(
                originalUrl,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(entityByOriginalUrl);
        
        _mockMapper.Setup(m => m.Map<ShortLinkEntity>(request))
            .Returns(entity);
        
        _mockShortLinkProvider.Setup(p => p.GenerateToken(tokenLength))
            .Returns(shortToken);
        
        _mockRepository.Setup(r => r.GetByTokenAsync(
                shortToken,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(entityByShortToken);
        
        _mockRepository.Setup(r => r.AddAsync(
                entity,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Result<ShortTheLinkResponseDto>>(response);
        Assert.NotNull(response.Value);
        Assert.IsType<ShortTheLinkResponseDto>(response.Value);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.Null(response.Error);
    }
    
    [Fact]
    private async Task ShortTheLink_ReturnsSuccessfulResult_WhenShortLinkIsAlreadyExists()
    {
        // Arrange
        const bool isSuccess = true;
        const string originalUrl = "originalUrl";
        const string baseUrl = "baseUrl";
        
        var request = new ShortTheLinkRequestDto(originalUrl);
        var command = new ShortTheLinkCommand(request, baseUrl);
        var entity = new ShortLinkEntity();
        
        _mockValidator.Setup(v => v.ValidateAsync(
                request,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        _mockRepository.Setup(r => r.GetByOriginalUrlAsync(
                originalUrl,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);

        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Result<ShortTheLinkResponseDto>>(response);
        Assert.NotNull(response.Value);
        Assert.IsType<ShortTheLinkResponseDto>(response.Value);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.Null(response.Error);
    }
    
    [Fact]
    private async Task ShortTheLink_ReturnsFailedResult_WithValidationError()
    {
        // Arrange
        const bool isSuccess = false;
        const string originalUrl = "originalUrl";
        const string baseUrl = "baseUrl";
        
        var request = new ShortTheLinkRequestDto(originalUrl);
        var command = new ShortTheLinkCommand(request, baseUrl);
        
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
        Assert.IsType<Result<ShortTheLinkResponseDto>>(response);
        Assert.IsType<ValidationError>(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Null(response.Value);
    }
    
    [Fact]
    private async Task ShortTheLink_ReturnsFailedResult_WithInternalError()
    {
        // Arrange
        const bool isSuccess = false;
        const int tokenLength = 7;
        const string originalUrl = "originalUrl";
        const string baseUrl = "baseUrl";
        const string shortToken = "shortToken";
        
        var request = new ShortTheLinkRequestDto(originalUrl);
        var command = new ShortTheLinkCommand(request, baseUrl);
        var entity = new ShortLinkEntity();
        ShortLinkEntity? entityByOriginalUrl = null;
        ShortLinkEntity? entityByShortToken = null;
        
        _mockValidator.Setup(v => v.ValidateAsync(
                request,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        _mockRepository.Setup(r => r.GetByOriginalUrlAsync(
                originalUrl,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(entityByOriginalUrl);
        
        _mockMapper.Setup(m => m.Map<ShortLinkEntity>(request))
            .Returns(entity);
        
        _mockShortLinkProvider.Setup(p => p.GenerateToken(tokenLength))
            .Returns(shortToken);
        
        _mockRepository.Setup(r => r.GetByTokenAsync(
                shortToken,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(entityByShortToken);
        
        _mockRepository.Setup(r => r.AddAsync(
                entity,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Result<ShortTheLinkResponseDto>>(response);
        Assert.IsType<InternalServerError>(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Null(response.Value);
    }
}
