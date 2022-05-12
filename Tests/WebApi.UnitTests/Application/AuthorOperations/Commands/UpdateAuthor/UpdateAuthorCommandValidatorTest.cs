using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", "", 0)]
        [InlineData(" ", " ", 1)]
        [InlineData(" ", "", -1)]
        [InlineData("", " ", 10)]
        [InlineData("a", "", -1)]
        [InlineData("ab", "", -5)]
        [InlineData("a", " ", 5)]
        [InlineData("ab", " ", 15)]
        [InlineData("", "a", 0)]
        [InlineData("", "ab", -1)]
        [InlineData(" ", "a", -10)]
        [InlineData(" ", "ab", 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname, int id)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null, null);
            command.AuthorId = id;
            command.Model = new UpdateAuthorViewModel
            {
                Name = name,
                Surname = surname,
                BirthDate = DateTime.Now.AddYears(-100)
            };
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {

            var command = new UpdateAuthorCommand(null, null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorViewModel
            {
                Name = "WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError_Name",
                Surname = "WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError_Surname",
                BirthDate = DateTime.Now
            };
            var validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("WhenValidInputsAreGiven_Name", "WhenValidInputsAreGiven_Surname", 1)]
        [InlineData("Name", "Surname", 2)]
        [InlineData("Oğuz", "Atay", 5)]
        [InlineData("Sebahattin", "Ali", 50)]
        [InlineData("Yahya Kemal", "Beyatlı", 100)]
        [InlineData("Aziz", "Nesin", 500)]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(string name, string surname, int id)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null, null);
            command.AuthorId = id;
            command.Model = new UpdateAuthorViewModel
            {
                Name = name,
                Surname = surname,
                BirthDate = DateTime.Now.AddYears(-100)
            };
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}