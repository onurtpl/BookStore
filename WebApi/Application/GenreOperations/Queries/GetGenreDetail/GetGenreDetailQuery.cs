using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperationOptions;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{

    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            Genre entity = _context.Genres.FirstOrDefault(x => x.Id == GenreId);
            if (entity is null)
                throw new InvalidOperationException("Kayıtlı Genre bulunamadı");

            var genreDetailViewModel = _mapper.Map<GenreDetailViewModel>(entity);
            return genreDetailViewModel;
        }

    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}