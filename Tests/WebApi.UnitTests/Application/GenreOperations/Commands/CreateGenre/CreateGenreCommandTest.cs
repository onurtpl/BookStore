using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperationOptions;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShoulBeReturn()
        {
            var genre = new Genre
            {
                Name = "Demo Genre"
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();

            var command = new CreateGenreCommand(_context, _mapper);
            command.Model = new CreateGenreModel
            {
                Name = genre.Name
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And
                .Message
                .Should()
                .Be("Genre zaten mevcut");
        }

        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldBeCreated()
        {
            var model = new Genre
            {
                Name = "WhenValidInputAreGiven_Genre_ShouldBeCreated"
            };

            var command = new CreateGenreCommand(_context, _mapper);
            command.Model = new CreateGenreModel
            {
                Name = model.Name
            };

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(x => x.Name == model.Name);

            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);

        }

    }
}