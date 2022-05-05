using System;
using FluentValidation;
using WebApi.Common;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(cmd => cmd.Id)
                .NotEmpty().WithMessage("Bu alan boş olamaz")
                .GreaterThan(0).WithMessage("Id değerinin 0'dan büyük olması gerekmektedir");
            RuleFor(cmd => cmd.Model.Title)
                .NotEmpty().WithMessage("Title alanının boş olmaması gerekmektedir")
                .MinimumLength(4).WithMessage("Title alanı en az 4 karkter içermelidir");
            RuleFor(cmd => cmd.Model.GenreId)
                .GreaterThan(0).WithMessage("GenreId değerinin 0'dan büyük olması gerekmektedir");
            RuleFor(cmd => cmd.Model.PageCount)
                .GreaterThan(0).WithMessage("PageCount değerinin 0'dan büyük olması gerekmektedir.");
            RuleFor(cmd => cmd.Model.PublishedDate)
                .NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}