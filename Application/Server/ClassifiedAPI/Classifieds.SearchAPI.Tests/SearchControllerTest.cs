#region Imports
using System;
using System.Collections.Generic;
using Moq;
using Classifieds.Search.BusinessEntities;
using Classifieds.Search.BusinessServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Classifieds.Common;
#endregion

namespace Classifieds.SearchAPI.Tests
{
    [TestClass]
    public class SearchControllerTest
    {
        #region Test Methods
        [TestMethod]
        public void Controller_FreeTextSearchTest()
        {
            //Arrange
            var mockService = new Mock<ISearchService>();
            var logger = new Mock<ILogger>();

            mockService.Setup(x => x.FullTextSearch(It.IsAny<string>()))
                .Returns(
                new List<Classified>
                { new Classified
                    {
                        Title ="title",
                        ListingType = "ListingType",
                        ListingCategory = "ListingCategory",
                        SubCategory = "SubCategory",
                        Address = "Address",
                        ContactNo = "ContactNo",
                        ContactName = "Contact Name",
                        Configuration = "Configuration"
                    }
                });

            logger.Setup(x => x.Log(It.IsAny<Exception>(),It.IsAny<string>()));

            var controller = new SearchAPI.Controllers.SearchController(mockService.Object, logger.Object);

            //Act
            List<Classified> objList = new List<Classified>();
            objList = controller.GetFullTextSearch("searchText");

            //Assert
            Assert.AreEqual(objList.Count, 1);
            Assert.AreEqual(objList[0].Title, "title");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Controller_FreeTextSearch_ThrowsException()
        {
            var mockService = new Mock<ISearchService>();
            var logger = new Mock<ILogger>();
            logger.Setup(x => x.Log(It.IsAny<Exception>(),It.IsAny<string>()));
            var controller = new SearchAPI.Controllers.SearchController(mockService.Object, logger.Object);
            var result = controller.GetFullTextSearch(null);
        }
        #endregion
    }
}
