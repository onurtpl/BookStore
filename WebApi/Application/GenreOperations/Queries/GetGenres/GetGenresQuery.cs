using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperationOptions;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenreViewModel> Handle()
        {
            List<Genre> entities = _context.Genres.OrderBy(x => x.Id).ToList<Genre>();
            // var vm = new List<GenreViewModel>();
            // foreach (var entity in entities)
            // {
            //     vm.Add(_mapper.Map<GenreViewModel>(entity));
            // }
            // return vm;
            var vm = _mapper.Map<List<GenreViewModel>>(entities);
            return vm;
        }

        
    }

    public class GenreViewModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}