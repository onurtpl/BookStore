using WebApi.DbOperationOptions;
using System.Linq;
using System;
using WebApi.Entities;
using AutoMapper;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        public int Id { get; set; }
        public UpdateBookModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateBookCommand(IBookStoreDbContext dbContext, IMapper mapper = null)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.FirstOrDefault(x => x.Id == Id);
            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı");

            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishedDate != default ? Model.PublishedDate : book.PublishDate;

            _dbContext.SaveChanges();
        }
    }


    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}