using System.Linq;
using System.Collections.Generic;
using WebApi.DbOperationOptions;
using WebApi.Models;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _context;
        public GetBooksQuery(BookStoreDbContext context)
        {
            _context = context;
        }

        public List<Book> Handle()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }
    }

    public class BooksViewModel 
    {
        public int MyProperty { get; set; }
    }
}