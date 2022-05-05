using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperationOptions;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public DeleteGenreCommand(BookStoreDbContext dbContext, IMapper mapper = null)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var entity = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
            if(entity is null)
                throw new InvalidOperationException("Kayıt bulunamadı");
            _dbContext.Genres.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}