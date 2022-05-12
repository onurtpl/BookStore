using System;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DbOperationOptions;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdGiven_InvalidOperationException_ShouldBeReturn()
        {
            var authorId = 100;
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = authorId;

            FluentActions.Invoking(() => query.Handle())
                    .Should()
                    .Throw<InvalidOperationException>()
                    .And
                    .Message
                    .Should()
                    .Be("Kayıt bulunamadı");
        }
    }
}