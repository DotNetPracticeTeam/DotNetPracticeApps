using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Adventure.Data.Infrastructure;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
namespace Adventure.Data.Tests
{
    [TestClass]
    public class AdventureDataTest
    {
        private IUnitOfWork _unit;
        public AdventureDataTest()
        {
            _unit = new UnitOfWork();
        }


        [TestMethod]
        public void AddPurchaseTest()
        {
            //delete Purchasing.PurchaseOrderDetail from Purchasing.PurchaseOrderDetail a left join Purchasing.PurchaseOrderHeader b on (a.PurchaseOrderID = b.PurchaseOrderID) where b.VendorID=1500
            //delete Purchasing.PurchaseOrderheader from Purchasing.PurchaseOrderHeader where VendorID=1500

            //delete Purchasing.PurchaseOrderDetail from Purchasing.PurchaseOrderDetail a left join Purchasing.PurchaseOrderHeader b on (a.PurchaseOrderID = b.PurchaseOrderID) where b.VendorID=1502
            //delete Purchasing.PurchaseOrderheader from Purchasing.PurchaseOrderHeader where VendorID=1502


           IUnitOfWork unit = new UnitOfWork();
           IPurchaseRepository repo = new PurchaseRepository(unit);

            var header = new PurchaseOrderHeader();
            header.EmployeeID = 1;
            header.RevisionNumber=10;
            header.Status=1;
            header.VendorID=1699;
            header.ShipMethodID = 1;
            header.OrderDate=DateTime.Now.Date;
            header.ShipDate=DateTime.Now.Date;
            header.SubTotal=555;
            header.TaxAmt=10;
            header.Freight=4;
            header.ModifiedDate = DateTime.Now.Date;

            header.PurchaseOrderDetails.Add(new PurchaseOrderDetail()
            {
                DueDate = DateTime.Now.Date
                ,
                OrderQty = 15
                ,
                ProductID = 998
                ,
                UnitPrice = 100
                ,
                LineTotal = 100
                ,
                ReceivedQty = 15
                ,
                RejectedQty = 0
                ,
                StockedQty = 0
                ,
                ModifiedDate = DateTime.Now.Date
            });

            header.PurchaseOrderDetails.Add(new PurchaseOrderDetail()
            {
                DueDate = DateTime.Now.Date
                ,
                OrderQty = 25
                ,
                ProductID = 999
                ,
                UnitPrice = 600
                ,
                LineTotal = 700
                ,
                ReceivedQty = 25
                ,
                RejectedQty = 0
                ,
                StockedQty = 0
                ,
                ModifiedDate = DateTime.Now.Date
            });
            
            repo.Add(header);

            header = repo.Get(x => x.ModifiedDate.Day == DateTime.Now.Day  &&
            x.ModifiedDate.Month == DateTime.Now.Month  &&
             x.ModifiedDate.Year == DateTime.Now.Year && x.VendorID == 1699);

            Assert.IsNotNull(header);
            Assert.IsTrue(header.PurchaseOrderDetails.Count() >= 2);
    
        }

        [TestMethod]
        public void UpdatePurchaseTest()
        {
            IUnitOfWork unit = new UnitOfWork();
           IPurchaseRepository repo = new PurchaseRepository(unit);

            var header = repo.Get(x => x.VendorID == 1699);

            Assert.IsNotNull(header);

            
            header.VendorID = 1700;

            var details = header.PurchaseOrderDetails.Where(x => x.ProductID == 999).AsQueryable();
            Assert.IsNotNull(details);

            var detail = details.FirstOrDefault();
            detail.ProductID= 997;
            detail.OrderQty = 300;
            var newdet = new PurchaseOrderDetail();
            newdet.DueDate = DateTime.Now.Date;
            newdet.ModifiedDate = DateTime.Now.Date;
            newdet.LineTotal = 10;
            newdet.OrderQty = 10;
            newdet.ProductID = 999;
            newdet.PurchaseOrderID = header.PurchaseOrderID;
            newdet.ReceivedQty = 0;
            newdet.RejectedQty = 0;
            newdet.StockedQty = 0;
            newdet.UnitPrice = 10;
            header.PurchaseOrderDetails.Add(newdet);
            header.PurchaseOrderDetails.Add(new PurchaseOrderDetail()
            {
                DueDate = DateTime.Now.Date
                ,
                OrderQty = 25
                ,
                ProductID = 998
                ,
                UnitPrice = 600
                ,
                LineTotal = 700
                ,
                ReceivedQty = 25
                ,
                RejectedQty = 0
                ,
                StockedQty = 0
                ,
                ModifiedDate = DateTime.Now.Date
            });




            repo.Update(header);

            header = repo.Get(x => x.VendorID == 1700);
            Assert.IsNotNull(header);

            details = header.PurchaseOrderDetails.Where(x => x.ProductID == 997).AsQueryable();
            Assert.IsNotNull(details);

            //var pocoDept = Mapper.Map<Department, dDepartment>(department);
            //return pocoDept;

        }


        [TestMethod]
        public void DeletePurchaseTest()
        {
            IUnitOfWork unit = new UnitOfWork();
           IPurchaseRepository repo = new PurchaseRepository(unit);
            repo.Delete (x=>x.VendorID==1700);
            var header = repo.Get(x => x.VendorID == 1700);
            Assert.IsNull(header);
        }



    }
}
