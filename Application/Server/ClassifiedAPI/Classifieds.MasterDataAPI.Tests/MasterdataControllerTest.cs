using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Classifieds.MastersData.BusinessEntities;
using Classifieds.MastersData.BusinessServices;
using Classifieds.MastersData.Repository;
using System.Collections.Generic;
using Classifieds.MasterDataAPI.Controllers;
using System.Collections;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Net;
using System.Web.Http;
using System.Web.Http.Routing;
using Classifieds.Common;

namespace Classifieds.MasterDataAPI.Tests
{
    [TestClass]
    public class MasterdataControllerTest
    {
        #region Unit Test Cases
        private ILogger _logger;
        [TestMethod]
        public void GetAllCategoryTest()
        {
            var mockService = new Mock<IMasterDataService>();

            mockService.Setup(x => x.GetAllCategory())
                .Returns(
                new List<MasterData>
                { new MasterData
                   {
                         ListingCategory = "test",
                        //SubCategory = "test"
                        
                   }
                });

            var controller = new MastersDataController(mockService.Object, _logger);

            //Act
            List<MasterData> objList = new List<MasterData>();
            objList = controller.GetAllCategory();

            //Assert
            Assert.AreEqual(objList.Count, 1);
            Assert.AreEqual(objList[0].SubCategory, "test");
        }

        [TestMethod]
        public void GetAllCategory_ReturnsList()
        {
            //Arrange
            var masterDataServiceMock = new Mock<IMasterDataService>();
            List<MasterData> masterdata = new List<MasterData>();
            MasterData testcategory = new MasterData();
            masterdata.Add(testcategory);

            //Act
            masterDataServiceMock.Setup(service => service.GetAllCategory());
            var controller = new MastersDataController(masterDataServiceMock.Object, _logger);
            var values = controller.GetAllCategory();

            //Assert
            Assert.AreEqual(values.Count, 1);
            Assert.AreEqual(values[0], testcategory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAllCategory_ThrowsException()
        {
            var mockService = new Mock<IMasterDataService>();
            var controller = new MastersDataController(mockService.Object, _logger);
            var result = controller.GetAllCategory();
        }

        [TestMethod]
        public void Controller_PostListTest_SetsLocationHeader()
        {
            // Arrange
            var mockService = new Mock<IMasterDataService>();
            mockService.Setup(x => x.CreateMasterData(It.IsAny<MasterData>()))
                .Returns(GetDataObject());
            MastersDataController controller = new MastersDataController(mockService.Object, _logger);

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost/api/MasterData")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "MastersData",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "MastersData" } });

            // Act
            MasterData listObj = GetDataObject();
            var response = controller.Post(listObj);

            // Assert
            Assert.AreEqual("http://localhost/api/MastersData", response.Headers.Location.AbsoluteUri);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(true, response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void Controller_PostListTest_SetsLocationHeader_MockURLHelperVersion()
        {
            // This version uses a mock UrlHelper.
            // Arrange
            var mockService = new Mock<IMasterDataService>();
            mockService.Setup(x => x.CreateMasterData(It.IsAny<MasterData>()))
                .Returns(GetDataObject());
            MastersDataController controller = new MastersDataController(mockService.Object, _logger);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            string locationUrl = "http://localhost/ListingsAPI/api/MasterData";

            // Create the mock and set up the Link method, which is used to create the Location header.
            // The mock version returns a fixed string.
            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns(locationUrl);
            controller.Url = mockUrlHelper.Object;

            // Act
            MasterData listObj = GetDataObject();
            var response = controller.Post(listObj);

            // Assert
            Assert.AreEqual(locationUrl, response.Headers.Location.AbsoluteUri);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(true, response.IsSuccessStatusCode);
        }

        private MasterData GetDataObject()
        {
            MasterData dataObject = new MasterData
            {

                ListingCategory = "test",
                //SubCategory = "test"

            };
            return dataObject;
        }

        [TestMethod]
        public void Controller_DeleteCategoryTest()
        {
            // Arrange
            var mockService = new Mock<IMasterDataService>();
            MasterData dataObject = GetDataObject();
            mockService.Setup(x => x.DeleteMasterdata(It.IsAny<string>()));
            var controller = new MastersDataController(mockService.Object, _logger);
            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("http://localhost/api/MasterData")
            };
            // Act                
            var response = controller.Delete(dataObject._id);

            //Assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            Assert.AreEqual(true, response.IsSuccessStatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Controller_DeleteList_ThrowsException()
        {
            var mockService = new Mock<IMasterDataService>();
            var controller = new MastersDataController(mockService.Object,_logger);
            var result = controller.Delete(null);
        }

        [TestMethod]
        public void Controller_UpdateMasterDataTest()
        {
            // Arrange
            var mockService = new Mock<IMasterDataService>();
            mockService.Setup(x => x.UpdateMasterData(It.IsAny<string>(), It.IsAny<MasterData>()))
                .Returns(GetDataObject());
            MastersDataController controller = new MastersDataController(mockService.Object, _logger);

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri("http://localhost/api/MasterData")
            };
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act     
            var dataObject = GetDataObject();
            var updatedProduct = new MasterData() { ListingCategory = dataObject.ListingCategory };
            var contentResult = controller.Put(dataObject._id, updatedProduct);

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            //Assert.That(listObject._id, Is.EqualTo("9")); // hasn't changed
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Controller_UpdateMasterData_ThrowsException()
        {
            var mockService = new Mock<IMasterDataService>();
            var controller = new MastersDataController(mockService.Object, _logger);
            var updatedProduct = new MasterData() { ListingCategory = "test" };
            var result = controller.Put(null, updatedProduct);
        }

        #endregion
    }
}
