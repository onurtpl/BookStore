using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbOperationOptions;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdsAreGiven_InvalidOperationException_ShoulBeReturn()
        {
            // arrange (hazırlık)
            var book = new Book
            {
                Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShoulBeReturn",
                PageCount = 100,
                PublishDate = new System.DateTime(1990, 01, 10),
                GenreId = 1,
                AuthorId = 2
            };

            UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
            command.Id = -1;
            command.Model = new UpdateBookModel
            {
                Title = book.Title
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı");
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var book = new Book
            {
                Title = "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                PageCount = 100,
                PublishDate = new System.DateTime(1990, 01, 10),
                GenreId = 1,
                AuthorId = 2
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
            command.Id = 1;

            UpdateBookModel bookModel = new UpdateBookModel
            {
                Title = book.Title,
                PageCount = 100,
                PublishedDate = new System.DateTime(1990, 01, 10),
                GenreId = 1,
                AuthorId = 2
            };
            command.Model = bookModel;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı başlığa sahip bir kitap zaten mevcut");

        }

        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBeCreated()
        {
            var book = new Book
            {
                Title = "WhenValidInputAreGiven_Book_ShouldBeCreated",
                PageCount = 100,
                PublishDate = new System.DateTime(1990, 01, 10),
                GenreId = 1,
                AuthorId = 2
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
            command.Id = book.Id;

            UpdateBookModel bookModel = new UpdateBookModel
            {
                Title = "new title",
                PageCount = book.PageCount + 1,
                PublishedDate = book.PublishDate.AddDays(-1),
                GenreId = book.GenreId,
                AuthorId = book.AuthorId
            };
            command.Model = bookModel;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var updatedBook = _context.Books.SingleOrDefault(x => x.Title == bookModel.Title);
            updatedBook.Should().NotBeNull();
            updatedBook.PageCount.Should().Be(bookModel.PageCount);
            updatedBook.GenreId.Should().Be(bookModel.GenreId);
            updatedBook.AuthorId.Should().Be(bookModel.AuthorId);
            updatedBook.Title.Should().Be(bookModel.Title);

        }

    }
}