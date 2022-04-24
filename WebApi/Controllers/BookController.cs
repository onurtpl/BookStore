using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>()
        {
            new Book
            {
                    Id = 1,
                    Title = "Lean Startup",
                    GenreId = 1,  // Personal Growth
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 01, 21)
            },
            new Book
            {
                    Id = 2,
                    Title = "HerLand",
                    GenreId = 2,  // Science Finction
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23)
            },
            new Book
            {
                    Id = 3,
                    Title = "Dune",
                    GenreId = 2,  // Science Finction
                    PageCount = 540,
                    PublishDate = new DateTime(2008, 05, 07)
            }
        };

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = BookList.FirstOrDefault(x => x.Id == id);
            return book;
        }

        // [HttpGet]
        // public Book Get([FromQuery]string id)
        // {
        //     var book = BookList.FirstOrDefault(x => x.Id == int.Parse(id));
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
            if(book is not null)
                return BadRequest();
            BookList.Add(newBook);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if(book is  null)
                return BadRequest();
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if(book is null)
                return BadRequest();
            BookList.Remove(book);
            return Ok();
        }
    }

}