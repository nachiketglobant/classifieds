using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassifiedsMongoService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedsMongoService.Controllers.Tests
{
    [TestClass()]
    public class ClassifiedsControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            ClassifiedsController controller = new ClassifiedsController();
            var result = controller.Get();
        }

        [TestMethod()]
        public void GetTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PostTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PutTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }
    }
}