using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure.Domain.Entities
{
    public interface IPurchaseHeader:IBaseEntity
    {
        int PurchaseOrderID { get; set; }
        Int16 RevisionNumber { get; set; }
        Int16 Status { get; set; }
        int EmployeeID { get; set; }
        int VendorID { get; set; }
        int ShipMethodID { get; set; }
        DateTime OrderDate { get; set; }
        DateTime ShipDate { get; set; }
        Decimal SubTotal { get; set; }
        Decimal TaxAmt { get; set; }
        Decimal Freight { get; set; }
        
        IQueryable<IPurchaseDetail> purchaseDetail { get; set; }
    }

}
