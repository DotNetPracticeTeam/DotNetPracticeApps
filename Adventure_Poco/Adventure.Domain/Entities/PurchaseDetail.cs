using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Adventure.Domain.Entities
{
    public class PurchaseDetail : IPurchaseDetail
    {
        public int PurchaseOrderID { get; set; }
        public int PurchaseOrderDetailID { get; set; }
        public DateTime DueDate { get; set; }
        public Int16 OrderQty { get; set; }
        public int ProductID { get; set; }
        public Decimal UnitPrice { get; set; }
        public Decimal LineTotal { get; set; }
        public Decimal ReceivedQty { get; set; }
        public Decimal RejectedQty { get; set; }
        public Decimal StockedQty { get; set; }
        public DateTime ModifiedDate { get; set; }
        public IPurchaseHeader purchaseHeader { get; set; }

       
    }
}
