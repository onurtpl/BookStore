using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperationOptions;
using FluentValidation.Results;
using WebApi.Models;
using FluentValidation;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(
            BookStoreDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // var book = _context.Books.FirstOrDefault(x => x.Id == id);
            // return book;
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            query.Id = id;
            try
            {
                var result = query.Handle();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // [HttpGet]
        // public Book Get([FromQuery]string id)
        // {
        //     var book = BookList.FirstOrDefault(x => x.Id == int.Parse(id));
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            
            try 
            {
                command.Model = newBook;

                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                // ValidationResult result = validator.Validate(command);
                // if(!result.IsValid)
                // {
                //     foreach (var item in result.Errors)
                //         Console.WriteLine($"Özellik: {item.PropertyName}, Hata Mesajı: {item.ErrorMessage}");
                // }
                // else
                //     command.Handle();
                validator.ValidateAndThrow(command);
                    
                command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            var command = new UpdateBookCommand(_context, _mapper);
            try
            {
                command.Model = updatedBook;
                command.Id = id;
                command.Handle();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var command = new DeleteBookCommand(_context);
            try
            {
                command.BookId = id;

                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);

                command.Handle();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}