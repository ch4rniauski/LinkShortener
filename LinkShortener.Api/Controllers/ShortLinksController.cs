using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Requests;
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

    [HttpPost("shorter")]
    public async Task<ActionResult> ShortTheLink(
        [FromBody] ShortTheLinkRequestDto request,
        CancellationToken cancellationToken)
    {
        
    }
}
