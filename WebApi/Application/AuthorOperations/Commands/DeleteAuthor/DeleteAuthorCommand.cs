using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperationOptions;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _context;
        public DeleteAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public void Handle()
        {
            var entity = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if(entity is null)
                throw new InvalidOperationException("Kayıt bulunamadı");
            _context.Authors.Remove(entity);
            _context.SaveChanges();
        }
    }
}