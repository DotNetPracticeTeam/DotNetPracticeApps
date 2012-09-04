using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Adventure.Data;
using Adventure.Data.Infrastructure;
using Adventure.Domain;
using Adventure.Service;
using Adventure.Web.ViewModel;


namespace Adventure.Web.Controllers
{
    public class DepartmentController : Controller
    {
        //
        // GET: /Department/
        private readonly IDepartmentService deptService;

       
        public DepartmentController(IDepartmentService deptService)
        {
            this.deptService = deptService;
            
        }
        


        public ActionResult Index()
        {

            var dept = deptService.GetAll();
            return View(dept);
        }

    }
}
