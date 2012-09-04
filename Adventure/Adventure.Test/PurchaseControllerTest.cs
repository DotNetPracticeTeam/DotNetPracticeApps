using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Adventure.Domain.Entities;
using Adventure.Service;
using Adventure.Web.Controllers;
using Adventure.Web.ViewModel;

using MvcContrib.TestHelper.Ui;
using MvcContrib.TestHelper.MockFactories;
using MvcContrib.TestHelper.Fakes;
using MvcContrib.TestHelper;


using Moq;
using System.Web.Mvc;


namespace Adventure.Test
{
    [TestClass]
    public class PurchaseControllerTest
    {

        private PurchaseController _purchasecontroller;
        private Mock<IPurchaseService> mockservice;
        [TestInitialize]
        public void Initialize()
        {
            TestControllerBuilder builder = new TestControllerBuilder();
            mockservice = new Mock<IPurchaseService>();
            _purchasecontroller = builder.CreateController<PurchaseController>(mockservice.Object);
        }

        [TestMethod]
        public void Default_Action_Returns_Index_View()
        {
            //arrange
            //nothing to be arranged

            //act
            var result = _purchasecontroller.Index();

            //assert
            result.AssertViewRendered().WithViewData<IQueryable<PurchaseHeaderModel>>();

        }

        [TestMethod]
        public void Default_Action_Returns_Create_View_With_Blank_Model()
        {
            //arrange
            //nothing to be arranged
            
            //act
            var result = _purchasecontroller.Create();

            //assert
            result.AssertViewRendered().WithViewData<PurchaseHeaderModel>();

        }

        [TestMethod]
        public void Create_Post_Action_Returns_Jason_WithValid_Model()
        {
            //arrange
            var model = StubPurchaseHeaderModel.ValidModel();
            
            //act
            var result = _purchasecontroller.Create(model);
            dynamic data = result.Data ;
            
            //assert
            Assert.AreEqual(data.Success,1);
        }

        [TestMethod]
        public void Create_Post_Action_Returns_Jason_WithInValid_Model()
        {
            //arrange
            var model = new PurchaseHeaderModel();
            
            _purchasecontroller.ModelState.AddModelError("Error in Model", "Error Message");
            //var exception= new ExceptionContext(_purchasecontroller.ControllerContext,new Exception("test message"));

            //act
            var result = _purchasecontroller.Create(model);
            dynamic data = result.Data;
            
            //assert
            
            Assert.AreEqual(data.Success, 0,"For invalid model Update / Insert should fails");

        }


       /* [TestMethod]
        public void Create_Post_Action_Exception_Returns_Jason_WithSuccess_Zero_And_Expception_Message()
        {
            //arrange
            var model = StubPurchaseHeaderModel.ValidModel();
          //  _purchasecontroller.ModelState.AddModelError("Error in Model",new FieldAccessException("Unable to access Field"));
            _purchasecontroller


            //act
            var result = _purchasecontroller.Create(model);
            dynamic data = result.Data;
            
            //assert
            //result.assert
            Assert.AreEqual(data.Success, 0, "For invalid model Update / Insert should fails");


        }*/

        [TestMethod]
        public void Edit_Action_Returns_Create_View_WithValid_Model()
        {

            //arrange
            var header = new PurchaseHeader();
            var detail = new PurchaseDetail();
            var dets = new List<PurchaseDetail>();
            dets.Add(detail);
            header.purchaseDetail =dets.AsQueryable() ;
            mockservice.Setup(x => x.GetPurchaseOrder(1)).Returns(header);

            //act
            var result = _purchasecontroller.Edit(1);
            
            //assert
            result.AssertViewRendered().ForView("Create");
            result.AssertViewRendered().WithViewData<PurchaseHeaderModel>();
        }


        [TestMethod]
        public void Create_Post_Action_For_Add_Returns()
        {
            //arrange
            var model = StubPurchaseHeaderModel.ValidModel();

            //act
            var result = _purchasecontroller.Create(model);
            dynamic data = result.Data;


            //assert
            Assert.AreEqual(data.Success, 1, "For Valid model Add operation should not fails");
            mockservice.Verify(x => x.AddPurchaseOrder(It.IsAny<IPurchaseHeader>()));
        }

        [TestMethod]
        public void Create_Post_Action_For_Update_Returns()
        {
            //arrange
            var model = StubPurchaseHeaderModel.ValidModel();
            model.PurchaseOrderID = 1;
            
            //act
            var result = _purchasecontroller.Create(model);
            dynamic data = result.Data;


            //assert
            Assert.AreEqual(data.Success, 1, "For Valid model Update should not fails");
            mockservice.Verify(x=>x.UpdatePurchaseOrder(1,It.IsAny<IPurchaseHeader>()));
        }

        [TestMethod]
        public void Details_Get_Action_Returns_Details_View_WithValid_Model()
        {
            //arrange
            var header = new PurchaseHeader();
            var detail = new PurchaseDetail();
            var dets = new List<PurchaseDetail>();
            dets.Add(detail);
            header.purchaseDetail = dets.AsQueryable();
            mockservice.Setup(x => x.GetPurchaseOrder(1)).Returns(header);

            var model = StubPurchaseHeaderModel.ValidModel();
            model.PurchaseOrderID = 1;

            //act
            var result = _purchasecontroller.Details(1);



            //assert
            result.AssertViewRendered().ForView("Details");
            result.AssertViewRendered().WithViewData<PurchaseHeaderModel>();
            Assert.IsInstanceOfType(result.AssertViewRendered().Model, typeof(PurchaseHeaderModel));
            mockservice.Verify(x => x.GetPurchaseOrder(1));
        }


        [TestMethod]
        public void DetailsEdit_Post_Action_On_Edit_Click_From_Details_View_Redirects_To_Edit_View()
        {
            //act
            var result = _purchasecontroller.DetailsEdit(1);



            //assert
            result.AssertActionRedirect().ToAction("Edit");
            
        }



        [TestMethod]
        public void Delete_Get_Action_Returns_Delete_Confirmation_View_WithValid_Model()
        {
            //arrange
            var header = new PurchaseHeader();
            var detail = new PurchaseDetail();
            var dets = new List<PurchaseDetail>();
            dets.Add(detail);
            header.purchaseDetail = dets.AsQueryable();
            mockservice.Setup(x => x.GetPurchaseOrder(1)).Returns(header);

            var model = StubPurchaseHeaderModel.ValidModel();
            model.PurchaseOrderID = 1;

            //act
            var result = _purchasecontroller.Delete(1);



            //assert
            result.AssertViewRendered().ForView("Delete");
            result.AssertViewRendered().WithViewData<PurchaseHeaderModel>();
            Assert.IsInstanceOfType(result.AssertViewRendered().Model, typeof(PurchaseHeaderModel));
            mockservice.Verify(x => x.GetPurchaseOrder(1));
        }



        [TestMethod]
        public void DeleteConfirmed_Post_Action_On_Delete_Click_From_Delete_View_Redirects_To_Index_View()
        {
            //act
            var result = _purchasecontroller.DeleteConfirmed(1);



            //assert
            result.AssertActionRedirect().ToAction("Index");

        }




        
    }
    public static class StubPurchaseHeaderModel
    {
        
        public static PurchaseHeaderModel ValidModel()
        {
            PurchaseHeaderModel  modelHeader = new PurchaseHeaderModel();
            PurchaseDetailModel modelDetail = new PurchaseDetailModel();
            var header = new PurchaseHeaderModel
            {
                PurchaseOrderID = 0,
                VendorID = 1502,
                ShipMethodID = 1,
                EmployeeID = 1,
                Status = 1,
                Freight = 10,
                ModifiedDate = DateTime.Now.Date,
                OrderDate = DateTime.Now.Date,
                RevisionNumber = 1,
                ShipDate = DateTime.Now.Date,
                SubTotal = 100,
                TaxAmt = 50
            };

            var detail = new PurchaseDetailModel { PurchaseOrderDetailID = 0, PurchaseOrderID = 0, DueDate = DateTime.Now.Date, LineTotal = 10, ModifiedDate = DateTime.Now.Date, ProductID = 1, OrderQty = 10, ReceivedQty = 0, RejectedQty = 0, StockedQty = 0, UnitPrice = 10 };
            var dets = new List<PurchaseDetailModel>();
            dets.Add(detail);
            header.purchasedetailmodel = dets;

            return header;
        }

        public static  PurchaseHeaderModel InValidModel()
        {
            PurchaseHeaderModel modelHeader = new PurchaseHeaderModel();
            PurchaseDetailModel modelDetail = new PurchaseDetailModel();

            var header = new PurchaseHeaderModel
            {
                PurchaseOrderID = 0,
                VendorID = 0,
                ShipMethodID = 0,
                EmployeeID = 0,
                Status = 5,
                Freight = 10,
                ModifiedDate = DateTime.Now.Date,
                OrderDate = DateTime.Now.Date,
                RevisionNumber = 1,
                ShipDate = DateTime.Now.Date,
                SubTotal = 100,
                TaxAmt = 50
            };

            var detail = new PurchaseDetailModel { PurchaseOrderDetailID = 0, PurchaseOrderID = 0, DueDate = DateTime.Now.Date, LineTotal = 10, ModifiedDate = DateTime.Now.Date, ProductID = 1, OrderQty = 10, ReceivedQty = 0, RejectedQty = 0, StockedQty = 0, UnitPrice = 10 };
            var dets = new List<PurchaseDetailModel>();
            dets.Add(detail);
            header.purchasedetailmodel = dets;

            return header; 
        }
    }
}
