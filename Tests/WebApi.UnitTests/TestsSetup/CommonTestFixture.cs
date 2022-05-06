using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperationOptions;

namespace WebApi.UnitTests.TestsSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context {get; set;}
        public IMapper Mapper {get; set;}

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase("BookStoreTestDb").Options;
            Context = new BookStoreDbContext(options);

            Context.Database.EnsureCreated();
            Context.AddAuthors();
            Context.AddGenres();
            Context.AddBooks();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => {cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }
}