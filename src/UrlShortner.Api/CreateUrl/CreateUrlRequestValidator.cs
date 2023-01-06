using FluentValidation;
using System;

namespace UrlShortner.Api.CreateUrl
{
    public class CreateUrlRequestValidator : Validator<CreateUrlRequest>
    {
        public CreateUrlRequestValidator()
        {
            RuleFor(x => x.Url)
                .NotNull()
                .NotEmpty()
                .WithMessage("Url must not be Null or empty");

            RuleFor(x => x.Url)
                .Must(p => Uri.TryCreate(p, UriKind.Absolute, out _))
                .WithMessage("Url Must be of proper format");
        }


       
    }
}
