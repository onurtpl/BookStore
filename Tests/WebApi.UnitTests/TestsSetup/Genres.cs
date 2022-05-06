using WebApi.DbOperationOptions;
using WebApi.Entities;

namespace WebApi.UnitTests.TestsSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
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
        }
    }
}