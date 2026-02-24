using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Requests;
using FluentValidation;

namespace ch4rniauski.LinkShortener.Application.Validators.ShortLink;

public sealed class UpdateLongLinkRequestDtoValidator : AbstractValidator<UpdateLongLinkRequestDto>
{
    public UpdateLongLinkRequestDtoValidator()
    {
        RuleFor(s => s.NewLongLink)
            .MaximumLength(2048)
            .NotEmpty();
    }
}
