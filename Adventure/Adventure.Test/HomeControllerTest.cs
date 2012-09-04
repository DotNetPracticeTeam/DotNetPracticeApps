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
    public class HomeControllerTest
    {

        private HomeController _homecontroller;

        [TestInitialize]
        public void Initialize()
        {
            TestControllerBuilder builder = new TestControllerBuilder();
            _homecontroller = builder.CreateController<HomeController>();
        }

        [TestMethod]
        public void Default_Action_Returns_Index_View()
        {
            //arrange
            //nothing to be arranged

            //act
            var result = _homecontroller.Index();

            //assert
            Assert.AreEqual(result.AssertViewRendered().ViewBag.Title,"Home Page");


        }

        [TestMethod]
        public void About_Action_Returns_Index_View()
        {
            //arrange
            //nothing to be arranged

            //act
            var result = _homecontroller.About();

            //assert
            Assert.AreEqual(result.AssertViewRendered().ViewBag.Title, "About Us");


        }
    }

}
