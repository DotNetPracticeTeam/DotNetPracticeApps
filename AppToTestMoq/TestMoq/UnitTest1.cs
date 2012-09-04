using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using AppToTestMoq;

namespace TestMoq
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<IRepository> mockRepository;
        private IService service;
        public UnitTest1()
        {
            mockRepository = new Mock<IRepository>();
            service = new CustomerService(mockRepository.Object);
 
        }

        [TestMethod]
        public void TestGetAll()
        {
            var custlist = new List<Customer>().AsQueryable();
            mockRepository.Setup(c => c.GetAll()).Returns(custlist);

            var customers = service.GetCustAll();

            Assert.IsNotNull(customers);
            Assert.IsInstanceOfType(customers, typeof(IQueryable<ICust>));
            mockRepository.VerifyAll();
        }


        [TestMethod]
        public void TestAdd()
        {
            var customer = new Cust() { Code=11,Name="Bharat"};
          
            mockRepository.Setup(c => c.Add(new Customer() { Code = 11, Name = "Bharat" }));

            service.AddCustomer(customer);
            
            mockRepository.Verify(m => m.Add(It.IsAny<Customer>()));
        }



    }
}
