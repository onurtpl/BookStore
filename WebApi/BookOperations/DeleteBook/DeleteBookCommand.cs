using System.Linq;
using System;
using WebApi.DbOperationOptions;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if(book is null)
                throw new InvalidOperationException("gönderilen id'ye ait kayıt bulunamadı");
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}