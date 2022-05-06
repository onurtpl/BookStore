using System.Linq;
using System;
using WebApi.DbOperationOptions;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        public DeleteBookCommand(IBookStoreDbContext dbContext)
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