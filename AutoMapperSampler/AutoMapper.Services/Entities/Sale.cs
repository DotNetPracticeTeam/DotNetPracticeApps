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

namespace Pubs.Services.Entities
{
    public  class Sale
    {
        
        public string StoreId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public short Quantity { get; set; }
        public string PaymentTerms { get; set; }
        public string TitleId { get; set; }
        public Store store { get; set; }
        public Title title { get; set; }
    }
}
