using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DbOperationOptions
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                    return;

                context.Genres.AddRange(
                    new Genre {
                        Name = "Personal Growth",
                    },
                    new Genre {
                        Name = "Science Finction",
                    },
                    new Genre {
                        Name = "Romance",
                    }
                );


                context.AddRange(
                    new Book
                    {
                        // Id = 1,
                        Title = "Lean Startup",
                        GenreId = 1,  // Personal Growth
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 01, 21)
                    },
                    new Book
                    {
                        // Id = 2,
                        Title = "HerLand",
                        GenreId = 2,  // Science Finction
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 05, 23)
                    },
                    new Book
                    {
                        // Id = 3,
                        Title = "Dune",
                        GenreId = 2,  // Science Finction
                        PageCount = 540,
                        PublishDate = new DateTime(2008, 05, 07)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}