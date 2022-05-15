using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperationOptions
{
    public interface IBookStoreDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<User> Users { get; set; }

        int SaveChanges();
    }
}