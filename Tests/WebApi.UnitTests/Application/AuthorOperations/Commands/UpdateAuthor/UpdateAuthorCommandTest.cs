using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DbOperationOptions;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdsAreGiven_InvalidOperationException_ShoulBeReturn()
        {
            var model = new UpdateAuthorViewModel
            {
                Name = "WhenInvalidIdsAreGiven_InvalidOperationException_ShoulBeReturn",
                Surname = "WhenInvalidIdsAreGiven_InvalidOperationException_ShoulBeReturn",
                BirthDate = DateTime.Now.AddYears(-50)
            };

            var command = new UpdateAuthorCommand(_context, _mapper);
            command.Model = model;
            command.AuthorId = 100;

            FluentActions.Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And
                .Message
                .Should()
                .Be("Kayıt bulunamadı");
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameAndSurnameGiven_InvalidOperationException_ShouldBeReturn()
        {
            var author = new Author
            {
                Name = "WhenAlreadyExistAuthorNameAndSurnameGiven_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenAlreadyExistAuthorNameAndSurnameGiven_InvalidOperationException_ShouldBeReturn",
                BirthDate = DateTime.Now.AddYears(-50)
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            var model = new UpdateAuthorViewModel
            {
                Name = author.Name,
                Surname = author.Surname,
                BirthDate = DateTime.Now.AddYears(-100)
            };
            
            var command = new UpdateAuthorCommand(_context, _mapper);
            command.AuthorId = 1;
            command.Model = model;

            FluentActions.Invoking(() => command.Handle())
                    .Should()
                    .Throw<InvalidOperationException>()
                    .And
                    .Message
                    .Should()
                    .Be("Aynı Name ve Surname değerine sahip author zaten mevcut");

        }
    
        [Fact]
        public void WhenValidInputAreGiven_Author_ShouldBeCreated()
        {
            var author = new Author
            {
                Name = "WhenValidInputAreGiven_Author_ShouldBeCreated_Name1",
                Surname = "WhenValidInputAreGiven_Author_ShouldBeCreated_Surname1",
                BirthDate = DateTime.Now.AddYears(-100)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            var authorId = author.Id;

            var model = new UpdateAuthorViewModel
            {
                Name = "WhenValidInputAreGiven_Author_ShouldBeCreated_Name2",
                Surname = "WhenValidInputAreGiven_Author_ShouldBeCreated_Surname2",
                BirthDate = DateTime.Now.AddYears(-99)
            };
            var command = new UpdateAuthorCommand(_context, _mapper);
            command.Model = model;
            command.AuthorId = authorId;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var updatedAuthor = _context.Authors.SingleOrDefault(x => x.Id == authorId);

            updatedAuthor.Should().NotBeNull();
            updatedAuthor.Name.Should().Be(model.Name);
            updatedAuthor.Surname.Should().Be(model.Surname);
            updatedAuthor.BirthDate.Should().Be(model.BirthDate);
        }
    }
}