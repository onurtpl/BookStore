using System;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Queries.GetBookById;
using WebApi.DbOperationOptions;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 10;

            // act & assert
            FluentActions.Invoking(() => command.Handle())
                    .Should()
                    .Throw<InvalidOperationException>()
                    .And
                    .Message
                    .Should()
                    .Be("gönderilen id'ye ait kayıt bulunamadı");
        }
    }
}