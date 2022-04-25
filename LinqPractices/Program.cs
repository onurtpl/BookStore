using System;
using System.Linq;
using LinqPractices.DbOperations;
using LinqPractices.DbOperstions;
using LinqPractices.Entities;

namespace LinqPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            DataGenerator.Initialize();
            LinqDbContext _context = new LinqDbContext();

            var students = _context.Students.ToList<Student>();
            

            // Find() -> primary key e göre ama yapılmasını sağlar
            var student = _context.Students.Find(3);

            // FirstOrDefault() -> kendisine gelen çoklu veri setinden ilkini getirir
            student = _context.Students.Where(x => x.Id == 2).FirstOrDefault();
            student = _context.Students.FirstOrDefault(x => x.Id == 3);
            // First - FirstOrDefault => First: eleman yoksa hata fırlatır, FirstOrDefault: eleman yoksa null döndürür

            // SingleOrDefault()  -> tanımlanan şarta göre 1 veya 0 veri bekler. Birden fazla veri dönerse hata fılatır  
            student = _context.Students.SingleOrDefault(x => x.Id == 1);

            // OrderBy -> verilen parametreye göre sıralar
            students = _context.Students.OrderBy(x => x.Name).ToList();

            // Anonymous Object Return
            var anonymousObject = _context.Students
                                        .Where(x => x.ClassId == 2)
                                        .Select(x => new {
                                            Id = x.Id,
                                            FullName = $"{x.Name} {x.Surname}"
                                        });
            foreach(var item in anonymousObject)
            {
                Console.WriteLine($"{item.Id} - {item.FullName}");
            }

        }
    }
}
