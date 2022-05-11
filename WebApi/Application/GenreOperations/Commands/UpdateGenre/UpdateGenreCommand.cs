using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperationOptions;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreViewModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateGenreCommand(IBookStoreDbContext dbContext, IMapper mapper = null)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var entity = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (entity is null)
                throw new InvalidOperationException("Kayıtlı Genre bulunamadı");
            if (_dbContext.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
                throw new InvalidOperationException("Aynı Name değerine sahip genre zaten mevcut");
            entity.Name = Model.Name != default ? Model.Name : entity.Name;

            _dbContext.SaveChanges();
        }
    }

    public class UpdateGenreViewModel
    {
        public string Name { get; set; }
    }
}