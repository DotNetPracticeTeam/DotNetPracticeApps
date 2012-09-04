using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure.Domain.Entities
{
    public interface IPurchaseDetail:IBaseEntity
    {
        int PurchaseOrderID { get; set; }
        int PurchaseOrderDetailID { get; set; }
        DateTime DueDate { get; set; }
        Int16 OrderQty { get; set; }
        int ProductID { get; set; }
        Decimal UnitPrice { get; set; }
        Decimal LineTotal { get; set; }
        Decimal ReceivedQty { get; set; }
        Decimal RejectedQty { get; set; }
        Decimal StockedQty { get; set; }
        
        
    }

}
