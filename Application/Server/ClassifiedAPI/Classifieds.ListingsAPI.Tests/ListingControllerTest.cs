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

        [TestMethod]
        public void GetListingsByCategory_ReturnsList()
        {
            //Arrange
            var listingServiceMock = new Mock<IListingService>();
            List<Listing> listings = new List<Listing>();
            Listing testListing = new Listing();
            testListing.ContactName = "Raju";
            testListing.ContactNo = "Main road facing";
            testListing.ExpiryDate = "13/02/2017";
            testListing.Furnished = "Non";
            testListing.ListingCategory = "Housing";
            testListing.ListingType = "Sell";
            testListing.Price = 4250000;
            testListing.Status = "Active";
            testListing.SubCategory = "2BHK";
            testListing.Submittedby = "Ashish";
            testListing.SubmittedDate = DateTime.Now.ToShortDateString();
            testListing.Title = "2BHK Flat";
            listings.Add(testListing);

            //Act
            listingServiceMock.Setup(service => service.GetListingsByCategory(It.IsAny<string>())).Returns(listings);
            var controller = new ListingsController(listingServiceMock.Object);
            var values = controller.GetListingsByCategory("Housing");

            //Assert
            Assert.AreEqual(values.Count, 1);
            Assert.AreEqual(values[0], testListing);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetListingByCategory_ThrowsException()
        {
            var mockService = new Mock<IListingService>();
            var controller = new ListingsController(mockService.Object);
            var result = controller.GetListingsByCategory(null);
        }
        #endregion
    }
}
