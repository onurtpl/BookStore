using System;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperationOptions;
using WebApi.Models;

namespace WebApi.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        public int Id { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public GetBookByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookViewModel Handle()
        {
            Book book = _dbContext.Books.FirstOrDefault(x => x.Id == Id);
            if(book is null)
                throw new InvalidOperationException("Kayıtlı kitap bulunamadı");
            var bookViewModel = new BookViewModel 
            {
                Title = book.Title,
                PageCount = book.PageCount,
                PublishedDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
                Genre = ((GenreEnum)book.GenreId).ToString()
            };
            return bookViewModel;
        }
    }

    public class BookViewModel 
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishedDate { get; set; }
        public string Genre { get; set; }
    }

    
}