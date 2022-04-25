using System;
using System.Linq;
using LinqPractices.DbOperations;
using LinqPractices.DbOperstions;

namespace LinqPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            DataGenerator.Initialize();
            LinqDbContext _context = new LinqDbContext();

            var students = _context.Students.ToList();
            foreach(var item in students)
            {
                Console.WriteLine($"Id: {item.Id} Name: {item.Name}, Surname: {item.Surname}");
            }
        }
    }
}
