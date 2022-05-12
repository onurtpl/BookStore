using System;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-100)]
        public void WhenInvalidIdsAreGiven_AuthorValidator_ShouldBeReturnErrors(int authorId)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            query.AuthorId = authorId;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void WhenValidIdsAreGiven_AuthorValidator_ShouldNotBeReturnErrors(int authorId)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            query.AuthorId = authorId;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }
    }
}