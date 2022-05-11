using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookById;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        [InlineData(-100)]
        [InlineData(-1000)]
        public void WhenInvalidIdAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(null, null);
            query.Id = id;

            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            var result =validator.Validate(query);

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
            GetBookByIdQuery query = new GetBookByIdQuery(null, null);
            query.Id = id;

            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            var result =validator.Validate(query);

            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}