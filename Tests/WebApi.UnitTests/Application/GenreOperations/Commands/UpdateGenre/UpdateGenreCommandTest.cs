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
    public class UpdateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShoulBeReturn()
        {
            var genre = new Genre
            {
                Name = "WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShoulBeReturn"
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context, _mapper);
            var model = new UpdateGenreViewModel
            {
                Name = genre.Name
            };
            command.GenreId = 1;
            command.Model = model;
            // Aynı Name değerine sahip genre zaten mevcut
            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And
                .Message
                .Should()
                .Be("Aynı Name değerine sahip genre zaten mevcut");
        }

        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldBeCreated()
        {
            var genre = new Genre
            {
                Name = "WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShoulBeReturn"
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context, _mapper);
            var model = new UpdateGenreViewModel
            {
                Name = "New Value"
            };
            command.GenreId = genre.Id;
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();
            var updatedGenre = _context.Genres.SingleOrDefault(x => x.Name == model.Name);

            updatedGenre.Should().NotBeNull();
            updatedGenre.Name.Should().Be(model.Name);
        }
    }
}