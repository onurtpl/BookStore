using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData(" ", "")]
        [InlineData("", " ")]
        [InlineData("a", "")]
        [InlineData("ab", "")]
        [InlineData("a", " ")]
        [InlineData("ab", " ")]
        [InlineData("", "a")]
        [InlineData("", "ab")]
        [InlineData(" ", "a")]
        [InlineData(" ", "ab")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            var birthDate =  DateTime.Now.Date.AddYears(-60);
            var model = new CreateAuthorViewModel
            {
                Name = name,
                Surname = surname,
                BirthDate = birthDate
            };
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = model;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            var birthDate = DateTime.Now;
            var model = new CreateAuthorViewModel
            {
                Name = "WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError",
                Surname = "WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError",
                BirthDate = birthDate
            };
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = model;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("Oğuz", "Atay")]
        [InlineData("Sebahattin", "Ali")]
        [InlineData("Yahya Kemal", "Beyatlı")]
        [InlineData("Aziz", "Nesin")]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(string name, string surname)
        {
            var birthDate =  DateTime.Now.Date.AddYears(-60);
            var model = new CreateAuthorViewModel
            {
                Name = name,
                Surname = surname,
                BirthDate = birthDate
            };
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = model;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}