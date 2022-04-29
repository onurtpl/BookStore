using FluentValidation;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(cmd => cmd.BookId).GreaterThan(0).WithMessage("Lütfen 0'dan büyük bir değer giriniz.");
        }
    }
}