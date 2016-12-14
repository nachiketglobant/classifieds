using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Classifieds.Listings.BusinessEntities;
using Classifieds.Listings.BusinessServices;
using Classifieds.Listings.Repository;
using System.Collections.Generic;
using Classifieds.ListingsAPI.Controllers;
using System.Collections;
namespace Classifieds.ListingsAPI.Tests
{
    [TestClass]
    public class ListingControllerTest
    {
        #region Unit Test Cases
        [TestMethod]
        public void GetListingByIdTest()
        {
            var mockService = new Mock<IListingService>();
            mockService.Setup(x => x.GetListingById(It.IsAny<string>()))
                .Returns(
                new List<Listing>
                { new Listing
                   {
                        ListingType = "test",
                        ListingCategory = "test",
                        SubCategory = "test",
                        Title = "test",
                        Address = "AAA",
                        ContactNo = "1111",
                        ContactName = "AAA AAA",
                        Configuration = "NA",
                        Details = "for rupees 20,000,000,000",
                        Brand = "test",
                        Price = 123,
                        YearOfPurchase = 123,
                        ExpiryDate = "test",
                        Status = "test",
                        Submittedby = "test",
                        SubmittedDate = "test",
                        IdealFor = "test",
                        Furnished = "test",
                        FuelType = "test",
                        KmDriven = 123,
                        YearofMake = 123,
                        Dimensions = "test",
                        TypeofUse = "test",
                        Photos = "test"
                   }
                });

            var controller = new ListingsController(mockService.Object);

            //Act
            List<Listing> objList = new List<Listing>();
            objList = controller.GetListingById("123");

            //Assert
            Assert.AreEqual(objList.Count, 1);
            Assert.AreEqual(objList[0].Title, "test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Controller_GetListingById_ThrowsException()
        {
            var mockService = new Mock<IListingService>();
            var controller = new ListingsController(mockService.Object);
            var result = controller.GetListingById(null);
        }
        #endregion
    }
}
