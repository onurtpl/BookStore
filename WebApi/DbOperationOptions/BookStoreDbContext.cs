using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.DbOperationOptions
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; } 
    }
}