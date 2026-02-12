using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace KooliProjekt.Application.Features.Kliendid
{
    [ExcludeFromCodeCoverage]
    public class SaveKlientCommandValidator : AbstractValidator<SaveKlientCommand>
    {
        public SaveKlientCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50);
        }
    }
}