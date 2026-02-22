using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Requests;
using FluentValidation;

namespace ch4rniauski.LinkShortener.Application.Validators.ShortLink;

internal sealed class ShortTheLinkRequestDtoValidator : AbstractValidator<ShortTheLinkRequestDto>
{
    public ShortTheLinkRequestDtoValidator()
    {
        RuleFor(s => s.OriginalUrl)
            .MaximumLength(2048)
            .NotEmpty();
    }
}
