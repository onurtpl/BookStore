using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperationOptions;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorViewModel Model { get; set; }

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var entity = _context.Authors.FirstOrDefault(x => x.Id == AuthorId);
            if(entity is null)
                throw new InvalidOperationException("Kayıt bulunamadı");
            entity.BirthDate = Model.BirthDate != default ? Model.BirthDate : entity.BirthDate;
            entity.Name = Model.Name != default ? Model.Name : entity.Name;
            entity.Surname = Model.Surname != default ? Model.Surname : entity.Surname;
            _context.SaveChanges();
        }
    }

    public class UpdateAuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}