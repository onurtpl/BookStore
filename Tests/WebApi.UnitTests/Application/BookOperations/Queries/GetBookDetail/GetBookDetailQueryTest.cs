using System;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookById;
using WebApi.DbOperationOptions;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            query.Id = 10;

            // act & assert
            FluentActions.Invoking(() => query.Handle())
                    .Should()
                    .Throw<InvalidOperationException>()
                    .And
                    .Message
                    .Should()
                    .Be("Kayıtlı kitap bulunamadı");
        }
    }
}