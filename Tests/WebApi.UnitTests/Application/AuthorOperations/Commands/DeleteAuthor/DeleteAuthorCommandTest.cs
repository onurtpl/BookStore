using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DbOperationOptions;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdGiven_InvalidOperationException_ShouldBeReturn()
        {
            var command = new DeleteAuthorCommand(_context, _mapper);
            command.AuthorId = 100;
            
            FluentActions.Invoking(() => command.Handle())
                    .Should()
                    .Throw<InvalidOperationException>()
                    .And.Message
                    .Should()
                    .Be("Kayıt bulunamadı");
        }
    }
}