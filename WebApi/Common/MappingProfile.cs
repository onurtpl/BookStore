using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookById;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ///
            ///     BOOKS
            ///
            // GetBooks
            CreateMap<Book, BookViewModel>()
                .ForMember(des => des.Genre, options => options.MapFrom(src => src.Genre.Name))
                .ForMember(des => des.Author, options => options.MapFrom(src => $"{src.Author.Name} {src.Author.Surname}"))
                .ForMember(des => des.PublishDate, options => options.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")));
            // GetBookById
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(des => des.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(des => des.Author, options => options.MapFrom(src => $"{src.Author.Name} {src.Author.Surname}"))
                .ForMember(des => des.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")));
            // CreateBook
            CreateMap<CreateBookModel, Book>();
            // UpdateBook
            CreateMap<UpdateBookModel, Book>();

            ///
            ///     Genre
            ///
            // GetGenresQuery
            CreateMap<Genre, GenreViewModel>();
            // GetGenreDetail
            CreateMap<Genre, GenreDetailViewModel>();
            // CreateGenre
            CreateMap<CreateGenreModel, Genre>();
            // UpdateGenre
            CreateMap<UpdateGenreViewModel, Genre>();

            ///
            ///     Author
            ///
            // GetAuthors
            CreateMap<Author, AuthorViewModel>()
                .ForMember(des => des.BirthDate, options => options.MapFrom(src => src.BirthDate.Date.ToString("dd/MM/yyyy")));
            // GetAuthorDetail
            CreateMap<Author, AuthorDetailViewModel>()
                .ForMember(des => des.BirthDate, options => options.MapFrom(src => src.BirthDate.Date.ToString("dd/MM/yyyy")));
            // Create Author
            CreateMap<CreateAuthorViewModel, Author>();
            // Update Author
            CreateMap<UpdateAuthorViewModel, Author>();
        }
    }
}