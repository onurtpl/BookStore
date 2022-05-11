using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperationOptions;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-100)]
        public void WhenInvalidIdsAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = id;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void WhenValidIdsAreGiven_Validator_ShouldNotBeReturnErrors(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = id;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

    }
}