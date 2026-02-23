using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Requests;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Responses;
using ch4rniauski.LinkShortener.Application.Extensions;
using ch4rniauski.LinkShortener.Application.UseCases.Commands.ShortLink;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ch4rniauski.LinkShortener.Api.Controllers;

[ApiController]
[Route("links")]
public class ShortLinksController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShortLinksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("shortener")]
    public async Task<ActionResult<ShortTheLinkResponseDto>> ShortTheLink(
        [FromBody] ShortTheLinkRequestDto request,
        CancellationToken cancellationToken)
    {
        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/links";
        
        var command = new ShortTheLinkCommand(request, baseUrl);
        
        var result = await _mediator.Send(command, cancellationToken);

        return result.Match(
            onSuccess: Ok,
            onFailure: err => Problem(
                detail: err.Description,
                statusCode: err.StatusCode));
    }

    [HttpGet("{token}")]
    public async Task<ActionResult> RedirectByShortLink(string token, CancellationToken cancellationToken)
    {
        var command = new GetOriginalUrlByShortTokenCommand(token);
        
        var result = await _mediator.Send(command, cancellationToken);
        
        return result.Match<RedirectByShortLinkResponse, ActionResult>(
            onSuccess: res => Redirect(res.OriginalUrl),
            onFailure: err => Problem(
                detail: err.Description,
                statusCode: err.StatusCode));
    }

    [HttpGet]
    public async Task<ActionResult> GetShortLinksByPage(
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 15)
    {
        
    }
}
