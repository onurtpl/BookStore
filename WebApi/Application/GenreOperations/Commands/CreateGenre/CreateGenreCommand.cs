using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperationOptions;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var entity = _dbContext.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if(entity is not null)
                throw new InvalidOperationException("Genre zaten mevcut");
            entity = _mapper.Map<Genre>(Model);
            _dbContext.Genres.Add(entity);
            _dbContext.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}