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
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateGenreCommand(BookStoreDbContext dbContext, IMapper mapper = null)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var entity = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
            if(entity is null) 
                throw new InvalidOperationException("Kayıtlı Genre bulunamadı");
            entity.Name = Model.Name != default ? Model.Name : entity.Name;

            _dbContext.SaveChanges();
        }
    }

    public class UpdateGenreViewModel
    {
        public string Name { get; set; }
    }
}