//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Pubs.Web.Models
{
    public  class EmployeeModel
    {
        public string Id { get; set; }
        public string EmployeeName { get; set; }
        public string publisherName { get; set; }
        public string JobDescription { get; set; }
        public JobModel jobmodel { get; set; }
    }
}
