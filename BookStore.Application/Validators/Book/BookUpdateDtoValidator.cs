using BookStore.Application.Dtos.Books;
using FluentValidation;

namespace BookStore.Application.Validators.Book;

public class BookUpdateDtoValidator : AbstractValidator<BookUpdateDto>
{
    public BookUpdateDtoValidator()
    {
        RuleFor(b => b.Title)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Title is required.")
            .NotEmpty().WithMessage("Title is required")
            .Length(2, 100).WithMessage("Title must be between 2 and 100 characters.");

        RuleFor(b => b.Description)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Description is required.")
            .NotEmpty().WithMessage("Description is required")
            .Length(2, 100).WithMessage("Description must be between 2 and 100 characters.");
    }
}