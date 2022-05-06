using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperationOptions;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly IMapper _mapper;
        private readonly IBookStoreDbContext _context;

        public int AuthorId { get; set; }

        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            Author entity = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if(entity is null)
                throw new InvalidOperationException("Kayıt bulunamadı");
            AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(entity);
            return vm;
        }
    }

    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
    }

}