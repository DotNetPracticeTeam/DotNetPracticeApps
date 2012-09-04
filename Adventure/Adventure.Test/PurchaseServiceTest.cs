using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Data.Objects;
using System.Data.Objects.DataClasses;

using Moq;

using Adventure.Service;
using Adventure.Data.Infrastructure;
using Adventure.Domain.Entities;
using Adventure.Data;
using System.Linq.Expressions;

namespace Adventure.Test
{
    [TestClass]
    public class PurchaseServiceTest
    {
        Mock<IPurchaseRepository> mockPurchaseRepository;
        Mock<IProductRepository> mockProductRepository ;
        Mock<IVendorRepository> mockVendorRepository ;
        Mock<IShipMethodRepository> mockShipMethodRepository;
        PurchaseService purcService;

        [TestInitialize]
        public void myinit()
        {
            mockPurchaseRepository = new Mock<IPurchaseRepository>(MockBehavior.Strict);
            mockProductRepository = new Mock<IProductRepository>(MockBehavior.Strict);
            mockVendorRepository = new Mock<IVendorRepository>(MockBehavior.Strict);
            mockShipMethodRepository = new Mock<IShipMethodRepository>(MockBehavior.Strict);
            purcService = new PurchaseService(mockPurchaseRepository.Object, mockProductRepository.Object, mockVendorRepository.Object, mockShipMethodRepository.Object);
 
        }

        public PurchaseServiceTest()
        {



        }


        [TestMethod]
        public void GetAllPurchaseOrderTest()
        {
            var orderheadersRepo = new List<PurchaseOrderHeader>();
            mockPurchaseRepository.Setup(x => x.GetAll()).Returns(orderheadersRepo.AsQueryable());
            var OrderHeaders = purcService.GetAll();
            Assert.IsNotNull(OrderHeaders);
            Assert.IsInstanceOfType(OrderHeaders, typeof(IQueryable<IPurchaseHeader>));
            mockPurchaseRepository.VerifyAll();
        }


        [TestMethod]
        public void GetPurchaseOrderTest()
        {
            mockPurchaseRepository.Setup(x => x.Get(It.IsAny<Expression<Func<PurchaseOrderHeader, bool>>>())).Returns(new PurchaseOrderHeader() { PurchaseOrderDetails = new EntityCollection<PurchaseOrderDetail>() });
            var OrderHeader=  purcService.GetPurchaseOrder(It.IsAny<Int32>());
            Assert.IsNotNull(OrderHeader);
            Assert.IsNotNull(OrderHeader.purchaseDetail);
            Assert.IsInstanceOfType(OrderHeader, typeof(IPurchaseHeader));
            Assert.IsInstanceOfType(OrderHeader.purchaseDetail, typeof(IQueryable<IPurchaseDetail>));
            mockPurchaseRepository.VerifyAll();
        }

        [TestMethod]
        public void AddPurchaseOrderTest()
        {
            
           
            var purdet = new PurchaseDetail();
            var _order = new PurchaseHeader();
            var dets = new List<PurchaseDetail>();
            dets.Add(purdet);
            _order.purchaseDetail = dets.AsQueryable();
            mockPurchaseRepository.Setup(x => x.Add(It.IsAny<PurchaseOrderHeader>()));

            purcService.AddPurchaseOrder(_order);


            mockPurchaseRepository.Verify(x => x.Add(It.IsAny<PurchaseOrderHeader>()));

        }


        [TestMethod]
        public void DeletePurchaseOrderTest()
        {


            mockPurchaseRepository.Setup(x => x.Delete(It.IsAny<Expression<Func<PurchaseOrderHeader,bool>>>()));

            purcService.DeletePurchaseOrder(1);

            mockPurchaseRepository.Verify(x => x.Delete(It.IsAny<Expression<Func<PurchaseOrderHeader, bool>>>()));

        }


        [TestMethod]
        public void UpdatePurchaseOrderTest()
        {

            var purdet = new PurchaseDetail();
            var _order = new PurchaseHeader();
            var dets = new List<PurchaseDetail>();
            dets.Add(purdet);
            _order.purchaseDetail = dets.AsQueryable();

            mockPurchaseRepository.Setup(x => x.Update(It.IsAny<PurchaseOrderHeader>()));

            purcService.UpdatePurchaseOrder(1,_order);

            mockPurchaseRepository.Verify(x => x.Update(It.IsAny<PurchaseOrderHeader>()));
          

        }

        [TestMethod]
        public void GetProductsTest()
        {

            var _products = new EntityCollection<Product>().AsQueryable();
            mockProductRepository.Setup(x => x.GetAll()).Returns(_products);
            var products = purcService.GetProducts();
            Assert.IsNotNull(products);
            Assert.IsInstanceOfType(products, typeof(IQueryable<IMaster>));
            mockProductRepository.Verify(x => x.GetAll());
        }


        [TestMethod]
        public void GetVendorsTest()
        {

            var _vendors = new EntityCollection<Vendor>().AsQueryable();
            mockVendorRepository.Setup(x => x.GetAll()).Returns(_vendors);
            var vendors = purcService.GetVendors();
            Assert.IsNotNull(vendors);
            Assert.IsInstanceOfType(vendors, typeof(IQueryable<IMaster>));
            mockVendorRepository.Verify(x => x.GetAll());
        }

        [TestMethod]
        public void GetShipMethodsTest()
        {

            var _shipmethods = new EntityCollection<ShipMethod>().AsQueryable();
            mockShipMethodRepository.Setup(x => x.GetAll()).Returns(_shipmethods);
            var shipmethods = purcService.GetShipMethods();
            Assert.IsNotNull(shipmethods);
            Assert.IsInstanceOfType(shipmethods, typeof(IQueryable<IMaster>));
            mockShipMethodRepository.Verify(x => x.GetAll());
        }

    }
}
