using System;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void WhenInvalidIdAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            var command = new DeleteAuthorCommand(null, null);
            command.AuthorId = id;

            var validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void WhenValidIdsAreGiven_Validator_ShouldNotBeReturnError(int id)
        {
            var command = new DeleteAuthorCommand(null, null);
            command.AuthorId = id;

            var validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
        
    }
}