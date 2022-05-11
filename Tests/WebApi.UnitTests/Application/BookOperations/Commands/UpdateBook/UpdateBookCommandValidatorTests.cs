using System;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        /*
        [Theory]
        [InlineData("Lord of the Rings", 0, 0)]
        [InlineData("Lord of the Rings", 0, 1)]
        [InlineData("", 0, 0)]
        [InlineData("  ", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData("Lor", 1, 1)]
        [InlineData("Lor", 0, 0)]
        [InlineData("Lor", 0, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            // arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel {
                Title = title,
                PageCount = pageCount,
                PublishedDate = DateTime.Now.AddYears(-1),
                GenreId = genreId
            };
            // act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel {
                Title = "title",
                PageCount = 100,
                PublishedDate = DateTime.Now.Date,
                GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("Lord of the Rings", 1, 1)]
        [InlineData("Lord of the Rings", 2, 1)]
        [InlineData("Denyo", 100, 1)]
        [InlineData("Lord", 100, 1)]
        [InlineData("Deneme", 100, 2)]
        [InlineData("Test 123", 1, 1)]
        [InlineData("Film", 1000, 1)]
        [InlineData("Eleman", 1, 15)]
        [InlineData("Teory", 10000, 1)]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(string title, int pageCount, int genreId)
        {
            // arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel {
                Title = title,
                PageCount = pageCount,
                PublishedDate = DateTime.Now.AddYears(-1),
                GenreId = genreId
            };
            // act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        */

    }
}