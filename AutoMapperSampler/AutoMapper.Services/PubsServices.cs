using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pubs.Services.Entities;
using Pubs.Data.Base;
using Pubs.Data.Interface;
using Pubs.Data;

using AutoMapper;

namespace Pubs.Services
{
    public interface IServices
    {
        Author GetAuthor();
        IEnumerable<Author> GetAuthors();
        IEnumerable<Title> GetTitles();
        IEnumerable<Sale> GetSales();
        IEnumerable<Job> GetEmployeeJobMapping();

    }
    public class PubsServices:IServices
    {

        private readonly IRepository<author> _AuthorRepo;
        private readonly IRepository<title> _TitleRepo;
        private readonly IRepository<titleauthor> _TitleAuthorRepo;
        private readonly IRepository<sale> _SaleRepo;
        private readonly IRepository<job> _JobRepo;
        public PubsServices(IRepository<author> authorrepo, IRepository<title> titlerepo, IRepository<titleauthor> titleauthorrepo, IRepository<sale> salerepo, IRepository<job> jobrepo)
        {
            _AuthorRepo = authorrepo;
            _TitleAuthorRepo = titleauthorrepo;
            _TitleRepo = titlerepo;
            _SaleRepo = salerepo;
            _JobRepo = jobrepo;

            Mapper.CreateMap<author, Author>();
            Mapper.CreateMap<title, Title>();
            Mapper.CreateMap<titleauthor, TitleAuthor>();
            Mapper.CreateMap<publisher, Publisher>();
            Mapper.CreateMap<sale, Sale>();
            Mapper.CreateMap<job, Job>();
            Mapper.CreateMap<employee, Employee>();
            Mapper.CreateMap<store, Store>();

        }


        public Author GetAuthor()
        {
            var authors = _AuthorRepo.First(x=>x.id==x.id);

            var Authors = Mapper.Map<author, Author>(authors);

            return Authors;
        }


        public IEnumerable<Author> GetAuthors()
        {
            var authors = _AuthorRepo.TopTen();
            var dtoAuthors = Mapper.Map<IEnumerable<author>,IEnumerable<Author>>(authors);
            return dtoAuthors;
        }
        public IEnumerable<Title> GetTitles()
        {
            return null;
        }


        public IEnumerable<Sale> GetSales()
        {
            var sales = _SaleRepo.TopTen();
            var dtoSales = Mapper.Map<IEnumerable<sale>, IEnumerable<Sale>>(sales);
            return dtoSales;
        }

        public IEnumerable<Job> GetEmployeeJobMapping()
        {
            var jobs = _JobRepo.TopTen();
            var dtoJobs = Mapper.Map<IEnumerable<job>, IEnumerable<Job>>(jobs);
            return dtoJobs;

        }


    }
}
