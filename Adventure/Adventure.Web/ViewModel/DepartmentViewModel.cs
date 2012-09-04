using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Adventure.Web.ViewModel
{
    public class DepartmentViewModel
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department Name Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "GroupName Required")]
        public string GroupName { get; set; }

        //[Required(ErrorMessage = "Date Required")]
        //public DateTime Date { get; set; }

        //[Required(ErrorMessage = "Amount Required")]
        //public double Amount { get; set; }

        //public IEnumerable<SelectListItem> Category { get; set; }

    }
}