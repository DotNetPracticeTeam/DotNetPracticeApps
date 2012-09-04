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
using System.Web.Routing;
using Adventure.Web;

namespace Adventure.Test
{
    [TestClass]
    public class PurchaseRoutesTest
    {


        [TestInitialize]
        public void Setup()
        {

            var routes = RouteTable.Routes;
            routes.Clear();
            MvcApplication.RegisterRoutes(routes);


        }

        [TestMethod]
        public void purchase_Index()
        {
            "~/Purchase"
                .ShouldMapTo<PurchaseController>(action => action.Index());
        }


        [TestMethod]
        public void purchase_Create_Get_Method()
        {
            "~/Purchase/Create"
              .WithMethod(HttpVerbs.Get)
              .ShouldMapTo<PurchaseController>(action => action.Create());
            
        }

        [TestMethod]
        public void purchase_Create_Post_Method()
        {
            "~/Purchase/Create"
              .WithMethod(HttpVerbs.Post)
              .ShouldMapTo<PurchaseController>(action => action.Create(It.IsAny<PurchaseHeaderModel>()));

        }

        [TestMethod]
        public void purchase_Edit_Get_Method()
        {
            "~/Purchase/Edit/1"
              .WithMethod(HttpVerbs.Get)
              .ShouldMapTo<PurchaseController>(action => action.Edit(1));
        }

        [TestMethod]
        public void purchase_Details_Get_Method()
        {
            "~/Purchase/Details/1"
              .WithMethod(HttpVerbs.Get)
              .ShouldMapTo<PurchaseController>(action => action.Details(1));

        }

        [TestMethod]
        public void purchase_Details_To_Edit_View_Post_Method()
        {
            "~/Purchase/Details/1"
              .WithMethod(HttpVerbs.Post)
              .ShouldMapTo<PurchaseController>(action => action.DetailsEdit(1));

        }


        [TestMethod]
        public void purchase_Delete_Get_Method()
        {
            "~/Purchase/Delete/1"
              .WithMethod(HttpVerbs.Get)
              .ShouldMapTo<PurchaseController>(action => action.Delete(1));

        }

        [TestMethod]
        public void purchase_Delete_To_Confirmation_View_Post_Method()
        {
            "~/Purchase/Delete/1"
              .WithMethod(HttpVerbs.Post)
              .ShouldMapTo<PurchaseController>(action => action.DeleteConfirmed(1));

        }


        [TestMethod]
        public void home_Index()
        {
            "~/Home"
                .ShouldMapTo<HomeController>(action => action.Index());
        }

        [TestMethod]
        public void home_About()
        {
            "~/Home/About"
                .WithMethod(HttpVerbs.Get)
                .ShouldMapTo<HomeController>(action => action.About());
        }



    }

}
