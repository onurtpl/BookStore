using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Queries.GetBookById;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        [InlineData(-100)]
        [InlineData(-1000)]
        public void WhenInvalidIdAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result =validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public void WhenValidIdsAreGiven_Validator_ShouldNotBeReturnErrors(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result =validator.Validate(command);

            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}