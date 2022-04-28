using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.Models;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>(); 
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(des => des.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
                .ForMember(des => des.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")));
            CreateMap<Book, BookViewModel>()
                .ForMember(des => des.Genre, options => options.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
                .ForMember(des => des.PublishDate, options => options.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")));
            CreateMap<UpdateBookModel, Book>();
                
        }
    }
}