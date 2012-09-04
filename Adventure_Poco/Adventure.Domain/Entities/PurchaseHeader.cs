using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Adventure.Domain.Entities
{
    public class PurchaseHeader : IPurchaseHeader
    {
        public int PurchaseOrderID { get; set; }
        public Int16 RevisionNumber { get; set; }
        public Int16 Status { get; set; }
        public int EmployeeID { get; set; }
        public int VendorID { get; set; }
        public int ShipMethodID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public Decimal SubTotal { get; set; }
        public Decimal TaxAmt { get; set; }
        public Decimal Freight { get; set; }
        public DateTime ModifiedDate { get; set; }
        public IQueryable<IPurchaseDetail> purchaseDetail { get; set; }
    }
}
