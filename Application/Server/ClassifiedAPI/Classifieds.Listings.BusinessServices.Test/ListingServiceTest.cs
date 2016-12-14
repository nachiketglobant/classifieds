using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Classifieds.Listings.BusinessEntities;
using Classifieds.Listings.Repository;
using Moq;

namespace Classifieds.Listings.BusinessServices.Test
{
    [TestClass]
    public class ListingServiceTest
    {
        #region Class Variables
        private Mock<IListingRepository> _moqAppManager;
        private IListingService _service;
        #endregion

        #region Initialize
        [TestInitialize]
        public void Initialize()
        {
            _moqAppManager = new Mock<IListingRepository>();
            _service = new ListingService(_moqAppManager.Object);
        }
        #endregion

        #region Setup
        private void SetUpClassifiedsListing()
        {
            var lstListing = new Listing()
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
            };

            var classifiedList = new List<Listing>();
            classifiedList.Add(lstListing);
            _moqAppManager.Setup(x => x.GetListingById(It.IsAny<string>())).Returns(classifiedList);
        }

        #endregion

        #region Unit Test Cases
        [TestMethod]
        public void GetListingByIdTest()
        {
            // Arrange
            SetUpClassifiedsListing();

            //Act
            var result = _service.GetListingById("123");

            //Assert
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void GetListingById_EmptyResult_Test()
        {
            // Arrange
            _moqAppManager.Setup(x => x.GetListingById(It.IsAny<string>())).Returns(new List<Listing>() { new Listing() });

            //Act
            var result = _service.GetListingById("123");

            //Assert
            Assert.IsNotNull(result[0], null);
            Assert.IsInstanceOfType(result, typeof(IList<Listing>));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Controller_GetListingById_ThrowsException()
        {
            var result = _service.GetListingById(null);
        }
        #endregion
    }
}
