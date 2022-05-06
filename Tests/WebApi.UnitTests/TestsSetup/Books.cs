using System;
using WebApi.DbOperationOptions;
using WebApi.Entities;

namespace WebApi.UnitTests.TestsSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                new Book
                {
                    // Id = 1,
                    Title = "Lean Startup",
                    GenreId = 1,  // Personal Growth
                    AuthorId = 1,
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 01, 21)
                },
                new Book
                {
                    // Id = 2,
                    Title = "HerLand",
                    GenreId = 2,  // Science Finction
                    AuthorId = 2,
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23)
                },
                new Book
                {
                    // Id = 3,
                    Title = "Dune",
                    GenreId = 2,  // Science Finction
                    AuthorId = 3,
                    PageCount = 540,
                    PublishDate = new DateTime(2008, 05, 07)
                }
            );
        }
    }
}