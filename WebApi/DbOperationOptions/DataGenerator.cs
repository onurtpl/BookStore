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

                context.Authors.AddRange(
                    new Author {
                        Name = "Hasan Ali",
                        Surname = "Yücel",
                        BirthDate = new DateTime(1900, 01, 23),
                    },
                    new Author {
                        Name = "Peyami",
                        Surname = "Safa",
                        BirthDate = new DateTime(1927, 06, 12),
                    },
                    new Author {
                        Name = "Aziz",
                        Surname = "Nesin",
                        BirthDate = new DateTime(1944, 03, 03),
                    }

                );


                context.AddRange(
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
                context.SaveChanges();
            }
        }
    }
}