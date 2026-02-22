using MediatR;

namespace ch4rniauski.LinkShortener.Application.UseCases.Commands.ShortLinks;

public sealed record ShortTheLinkCommand() : IRequest<>;
