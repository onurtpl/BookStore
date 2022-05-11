using System.Runtime.Serialization;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperationOptions;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("abc")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreModel
            {
                Name = name
            };
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("WhenValidInputsAreGiven_GenreValidator_ShouldNotBeReturnErrors")]
        [InlineData("Demo 1")]
        [InlineData("Demo 2")]
        [InlineData("Demo 3")]
        public void WhenValidInputsAreGiven_GenreValidator_ShouldNotBeReturnErrors(string name)
        {
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreModel
            {
                Name = name
            };
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}