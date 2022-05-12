using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DbOperationOptions;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameSurnameareGiven_InvalidOperationException_ShoulBeReturn()
        {
            var author = new Author
            {
                Name = "WhenAlreadyExistAuthorNameSurnameareGiven_InvalidOperationException_ShoulBeReturn_Name",
                Surname = "WhenAlreadyExistAuthorNameSurnameareGiven_InvalidOperationException_ShoulBeReturn_Surname",
                BirthDate = DateTime.Now.Date.AddYears(50)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            var authorModel = new CreateAuthorViewModel
            {
                Name = author.Name,
                Surname = author.Surname,
                BirthDate = DateTime.Now.Date.AddYears(-51)
            };
            command.Model = authorModel;

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And
                .Message
                .Should()
                .Be("Böyle bir kayıt zaten mevcut");
        }

        [Fact]
        public void WhenValidNameAndSurnameAreGiven_Author_ShouldNotBeReturnErrors()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            var authorModel = new CreateAuthorViewModel
            {
                Name = "WhenValidNameAndSurnameAreGiven_Author_ShouldNotBeReturnErrors_Name",
                Surname = "WhenValidNameAndSurnameAreGiven_Author_ShouldNotBeReturnErrors_Surname",
                BirthDate = DateTime.Now.Date.AddYears(50)
            };
            command.Model = authorModel;
            FluentActions.Invoking(() => command.Handle()).Invoke();

            var author = _context
                            .Authors    
                            .SingleOrDefault(x => x.Name == authorModel.Name  && x.Surname == authorModel.Surname);
            
            author.Should().NotBeNull();
            author.Name.Should().Be(authorModel.Name);
            author.Surname.Should().Be(authorModel.Surname);
            author.BirthDate.Should().Be(authorModel.BirthDate);
        }
    }
}