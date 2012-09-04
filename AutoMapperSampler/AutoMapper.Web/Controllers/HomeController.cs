using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Pubs.Services;
using Pubs.Services.Entities;
using Pubs.Web.Models;
using System.Data;

using AutoMapper;

namespace Pubs.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServices _pubservices;
        public HomeController(IServices pubservices)
        {
            _pubservices = pubservices;

        }


        public ActionResult Index()
        {
            ViewBag.Message = ".NET Dev Forum : AutoMapper Presentation";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        //******************
        //NORMAL MAPPING
        //*******************
        public ActionResult ManualMapping()
        {
            ViewBag.Title = "Manual Mapping";
            ViewBag.ViewType = "Manual Mapping";
            List<Author> authorObj = _pubservices.GetAuthors().ToList();
            List<AuthorModel> authormodels = new List<AuthorModel>();    
            foreach (var item in authorObj)
            {
                authormodels.Add(new AuthorModel{
                    
                        FirstName = item.FirstName,
                        LastName= item.LastName,
                        Id= item.Id,
                        Phone= item.Phone,
                        State= item.State
                });
            }
            
            return View("AuthorList",authormodels);

        }


        //******************
        //NORMAL MAPPING
        //*******************
        public ActionResult AuthorList()
        {
            Mapper.CreateMap<Author, AuthorModel>();
            //******************
            ViewBag.Title = "Plain Mapping";
            ViewBag.ViewType = "Plain Mapping";
            var model = Mapper.Map<IEnumerable<Author>, IEnumerable<AuthorModel>>(_pubservices.GetAuthors());
            return View(model);

        }





        //************************
        //FLATTENING with Store and Sale Entity
        //************************
        public ActionResult SalesList()
        {
            Mapper.CreateMap<Sale, SaleModel>();
            //*************************
            ViewBag.Title = "Flattening";
            ViewBag.ViewType = "FLATTENING";
            var _listSalesModel = Mapper.Map<IEnumerable<Sale>, IEnumerable<SaleModel>>(_pubservices.GetSales());
            return View(_listSalesModel);
        }




        //**************************
        //PROJECTION
        //*******************
        public ActionResult AuthorListWithProjection()
        {

            Mapper.CreateMap<Author, AuthorProjectionModel>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.FirstName + ' ' + src.LastName));
            //***************************
            ViewBag.Title = "Projection";
            ViewBag.ViewType = "PROJECTION";
            var model = Mapper.Map<IEnumerable<Author>, IEnumerable<AuthorProjectionModel>>(_pubservices.GetAuthors());
            return View(model);

        }

        //*******************
        //NESTED MAPPINGS with PROJECTION
        //*******************
        public ActionResult EmployeeJobWithNestedMapping()
        {
            Mapper.CreateMap<Job, JobModel>();
            Mapper.CreateMap<Employee, EmployeeModel>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.FirstName + ' ' + src.LastName));
            

            var model = Mapper.Map<IEnumerable<Job>, IEnumerable<JobModel>>(_pubservices.GetEmployeeJobMapping());
            ViewBag.Title = "Nested Mapping";
            return View(model);

        }


        //************************
        //CUSTOM TYPE CONVERTER 
        //************************
        public ActionResult CustomTypeConverter()
        {
            Mapper.CreateMap<Sale, SaleModel>();
            Mapper.CreateMap<DateTime, string>().ConvertUsing<DateTimeTypeConverter>();
            
            var _listSalesModel = Mapper.Map<IEnumerable<Sale>, IEnumerable<SaleModel>>(_pubservices.GetSales());
            ViewBag.Title = "Custom Type Converter";
            ViewBag.ViewType = "CUSTOMTYPECONVERTER";
            return View("SalesList",_listSalesModel);
        }

        //RESOLVEUSING
        public ActionResult ResolveUsing(int resolvetype)
        {
            switch (resolvetype)
            {
                case 1:
                    //ResolveUsing with Passing Type and Automapper will create Instance and use it at runtime. 
                   Mapper.CreateMap<Author, AuthorProjectionModel>()
                        .ForMember(dest => dest.AuthorName, opt => opt.ResolveUsing<AuthorNameResolver>());
                    ViewBag.Title = "[ResolveUsing with Passing Type] and Automapper will create Instance and use it at runtime.";
                    ViewBag.ViewType = "RESOLVE1";
                    break;
                case 2:
                    //ResolveUsing with Create and Pass a instance to Automapper
                    Mapper.CreateMap<Author, AuthorProjectionModel>()
                    .ForMember(dest => dest.AuthorName, opt => opt.ResolveUsing( new AuthorNameResolver()));
                    ViewBag.Title = "[ResolveUsing with Create and Pass a instance] to Automapper";
                    ViewBag.ViewType = "RESOLVE2";
                    break;
                case 3:
                    //ResolveUsing with ContructedBy 
                    //AutoMapper will execute this callback function instead of 
                    //using reflection during the mapping operation, helpful in scenarios where the resolver might 
                    //have constructor arguments or need to be constructed by an IoC container
                    Mapper.CreateMap<Author, AuthorProjectionModel>()
                    .ForMember(dest => dest.AuthorName, opt => opt.ResolveUsing<AuthorNameResolver>().ConstructedBy(() => new AuthorNameResolver()));
                    ViewBag.Title = "[ResolveUsing with ContructedBy] using reflection during the mapping operation, helpful in scenarios where the resolver might have constructor arguments or need to be constructed by an IoC container";
                    ViewBag.ViewType = "RESOLVE3";
                    break;
                default:
                    //ResolveUsing with Passing Type and Automapper will create Instance and use it at runtime. 
                    Mapper.CreateMap<Author, AuthorProjectionModel>()
                         .ForMember(dest => dest.AuthorName, opt => opt.ResolveUsing<AuthorNameResolver>());
                    ViewBag.Title = "[ResolveUsing with Passing Type] and Automapper will create Instance and use it at runtime.";
                    ViewBag.ViewType = "RESOLVE1";
                    break;
            }

            var model = Mapper.Map<IEnumerable<Author>, IEnumerable<AuthorProjectionModel>>(_pubservices.GetAuthors());
            return View("AuthorListWithProjection", model);


        }



        //****************************
        //CUSTOM TYPE CONVERTERS
        ////Mapper.CreateMap<string, int>().ConvertUsing(Convert.ToInt32);
        ////Mapper.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
        ////Mapper.CreateMap<string, Type>().ConvertUsing<TypeTypeConverter>();
        //Mapper.CreateMap<DateTime, string>().ConvertUsing<DateTimeTypeConverter>();
        //*****************************

        //************************
        //CUSTOM TYPE CONVERTER
        //DateTime formatted to LongDateTime String.
        //public ActionResult SalesListWithCustomTypeConverter()
        //{
        //    Mapper.CreateMap<Sale, SaleModel>();
        //    Mapper.CreateMap<DateTime, string>().ConvertUsing<DateTimeTypeConverter>();
        //    ViewBag.Title = "CUSTOMTYPECONVERTER";
        //    var _listSalesModel = Mapper.Map<IEnumerable<Sale>, IEnumerable<SaleModel>>(_pubservices.GetSales());
        //    return View(_listSalesModel);
        //}




    }

    public class AuthorNameResolver : ValueResolver<Author, string>
    {
        protected override string ResolveCore(Author source)
        {

            return source.FirstName + " " + source.LastName;
        }

    }


    public class DateTimeTypeConverter : ITypeConverter<DateTime, string>
    {
        public string Convert(ResolutionContext context)
        {
            return System.Convert.ToDateTime(context.SourceValue).ToLongDateString();
            
        }
    }


}
