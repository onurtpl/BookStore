using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DbOperationOptions;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("abc")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model = new UpdateGenreViewModel
            {
                Name = name
            };
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("WhenValidIdsAreGiven_Validator_ShouldNotBeReturnErrors")]
        [InlineData("Demo 1")]
        [InlineData("Demo 2")]
        public void WhenValidIdsAreGiven_Validator_ShouldNotBeReturnErrors(string name)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model = new UpdateGenreViewModel
            {
                Name = name
            };
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}