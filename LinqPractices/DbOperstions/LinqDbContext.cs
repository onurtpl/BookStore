using LinqPractices.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinqPractices.DbOperations
{
    public class LinqDbContext : DbContext
    {
        // public LinqDbContext(DbContextOptions<LinqDbContext> options) : base(options) {}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "LinqDB");
        }
        public DbSet<Student> Students { get; set; }
    }
}