using System;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("", 0, 1, 1, 1)]
        [InlineData(" ", -5, 1, 1, 1)]
        [InlineData("", 1, 1, 1, 1)]
        [InlineData("Lor", 1,  1, 1, 1)]
        [InlineData("Lord", 10, 0, 1, 1)]
        [InlineData("Demo 123", 10, -5, 1, 1)]
        [InlineData("Demo 1233", 5, 1, 0, 1)]
        [InlineData("Demo", 5, 1, -3, 1)]
        [InlineData("Lord Of The Rings", 2, 1, 1, 0)]
        [InlineData( "Lord Of The Rings", 1, 1, 1, -10)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId, int bookId)
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Id = bookId;
            command.Model = new UpdateBookModel { Title = title, PageCount = pageCount, PublishedDate = System.DateTime.Now.AddMonths(3), GenreId = genreId, AuthorId = authorId };

            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Id = 1;
            command.Model = new UpdateBookModel { Title = "Demo", PageCount = 100, PublishedDate = System.DateTime.Now.Date, GenreId = 1, AuthorId = 1 };

            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Id = 1;
            command.Model = new UpdateBookModel { Title = "Demo", PageCount = 100, PublishedDate = System.DateTime.Now.Date.AddYears(-2), GenreId = 1, AuthorId = 1 };

            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }


    }
}