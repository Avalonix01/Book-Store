using BookStore.Application.Dtos.Authors;
using FluentValidation;

namespace BookStore.Application.Validators.Author;

public class AuthorUpdateDtoValidator : AbstractValidator<AuthorUpdateDto>
{
    public AuthorUpdateDtoValidator()
    {
        RuleFor(a => a.Name)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Name is required.")
            .NotEmpty().WithMessage("Name cannot be empty.")
            .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");

        RuleFor(a => a.Surname)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Surname is required.")
            .NotEmpty().WithMessage("Surname cannot be empty.")
            .Length(2, 50).WithMessage("Surname must be between 2 and 50 characters.");
    }
}