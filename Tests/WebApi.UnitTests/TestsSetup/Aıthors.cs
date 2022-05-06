using System;
using WebApi.DbOperationOptions;
using WebApi.Entities;

namespace WebApi.UnitTests.TestsSetup
{
    public static class Aıthors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
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
        }
    }
}