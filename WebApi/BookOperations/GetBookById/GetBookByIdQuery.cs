using System;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DbOperationOptions;
using WebApi.Models;

namespace WebApi.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        public int Id { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBookByIdQuery(BookStoreDbContext dbContext, IMapper mapper = null)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            Book book = _dbContext.Books.FirstOrDefault(x => x.Id == Id);
            if (book is null)
                throw new InvalidOperationException("Kayıtlı kitap bulunamadı");
            var bookViewModel = _mapper.Map<BookDetailViewModel>(book);
            return bookViewModel;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }


}