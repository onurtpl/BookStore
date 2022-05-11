using System;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperationOptions;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 1000;

            FluentActions.Invoking(() => command.Handle())
                    .Should()
                    .Throw<InvalidOperationException>()
                    .And
                    .Message
                    .Should()
                    .Be("Kayıt bulunamadı");
        }
    }
}