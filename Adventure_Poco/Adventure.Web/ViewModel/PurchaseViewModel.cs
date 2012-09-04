using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adventure.Web.ViewModel
{
    public class PurchaseDetailModel 
    {
        public PurchaseDetailModel()
        {
            ReceivedQty = 0;
            RejectedQty = 0;
            StockedQty = 0;
            DueDate = DateTime.Now.Date;
            ModifiedDate = DateTime.Now.Date;
        }

        [Key, Column(Order=0)]
        public int PurchaseOrderID { get; set; }
        [Key, Column(Order = 1)]
        public int PurchaseOrderDetailID { get; set; }
        public DateTime DueDate { get; set; }
        
        [Required(ErrorMessage = "Order Quantity Required")]
        public Int16 OrderQty { get; set; }
        
        [Required(ErrorMessage = "Product Required")]
        public int ProductID { get; set; }
        
        [Required(ErrorMessage = "Unit Price Required")]
        public Decimal UnitPrice { get; set; }
        public Decimal LineTotal { get; set; }
        public Decimal ReceivedQty { get; set; }
        public Decimal RejectedQty { get; set; }
        public Decimal StockedQty { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual PurchaseHeaderModel purchaseheadermodel { get; set; }
    
    }

    public class PurchaseHeaderModel
    {
        
        public PurchaseHeaderModel()
        {
            RevisionNumber = 0;
            Status = 1;
            EmployeeID = 1;
            ModifiedDate = DateTime.Now.Date;
            ShipDate = DateTime.Now.Date;
            OrderDate = DateTime.Now.Date;
            //SubTotal = purchasedetailmodel.Sum(x => x.LineTotal);
        }

        [Key]
        public int PurchaseOrderID { get; set; }
        public Int16 RevisionNumber { get; set; }

        [Range(1, 4)]
        public Int16 Status { get; set; }
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Vendor Required")  ]
        [Range(1,int.MaxValue)]
        public int VendorID { get; set; }

        [Required(ErrorMessage = "Ship Required")]
        [Range(1, int.MaxValue)]
        public int ShipMethodID { get; set; }

        [Required(ErrorMessage = "Order Date Required")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Ship Date Required")]
        public DateTime ShipDate { get; set; }


        public Decimal SubTotal { get; set; }

        [Required(ErrorMessage = "Tax Amt Required")]
        public Decimal TaxAmt { get; set; }
        
        [Required(ErrorMessage = "Freight Required")]
        public Decimal Freight { get; set; }

        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<PurchaseDetailModel> purchasedetailmodel { get; set; }

        public IEnumerable<SelectListItem> VendorList { get; set; }
        public IEnumerable<SelectListItem> ShipMethodList { get; set; }

        

    }

    public class MasterModel
    {
        int MasterID { get; set; }
        string MasterName { get; set; }
    }


    public class PurchaseModel
    {
        public PurchaseHeaderModel header { get; set; }
        public IQueryable<PurchaseDetailModel> details { get; set; }
        public IQueryable<MasterModel> VendorList { get; set; }
        public IQueryable<MasterModel> ShipMethodList { get; set; }
        public IQueryable<MasterModel> ProductList { get; set; }
        
    }
}