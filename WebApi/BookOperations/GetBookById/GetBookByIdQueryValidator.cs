using FluentValidation;

namespace WebApi.BookOperations.GetBookById
{
    public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator()
        {
            RuleFor(cmd => cmd.Id).GreaterThan(0).WithMessage("Lütfen 0'dan büyük bir değer giriniz.");
        }
    }
}