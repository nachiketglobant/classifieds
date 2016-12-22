using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Classifieds.Listings.Repository;
using Classifieds.Listings.BusinessEntities;
using System.Collections.Generic;

namespace Classifieds.Listings.Repository.Tests
{
    [TestClass]
    public class ListingRepositoryTest
    {
        #region Class Variables
        private IListingRepository _customerRepo;
        private IDBRepository _dbRepository;
        private List<Listing> classifiedList = new List<Listing>();
        #endregion

        #region Initialize
        [TestInitialize]
        public void Initialize()
        {
            _dbRepository = new DBRepository();
            _customerRepo = new ListingRepository(_dbRepository);
            
        }
        #endregion

        #region Setup
        private void SetUpClassifiedsListing()
        {
            var lstListing = GetListObject();
            classifiedList.Add(lstListing);
        }

        private Listing GetListObject()
        {
            Listing listObject = new Listing
            {
                //_id = "9",
                ListingType = "sale",
                ListingCategory = "Housing",
                SubCategory = "2 bhk",
                Title = "flat on rent",
                Address = "pune",
                ContactNo = "12345",
                ContactName = "AAA AAA",
                Configuration = "NA",
                Details = "for rupees 49,00,000",
                Brand = "Kumar",
                Price = 90,
                YearOfPurchase = 2000,
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
            return listObject;
        }
        #endregion

        #region Unit Test Cases
        [TestMethod]
        public void Repo_GetListingByIdTest()
        {
            // Arrange
            SetUpClassifiedsListing();

            //Act
            var result = _customerRepo.GetListingById("5855490f54de7c20a8f52b19");

            //Assert
            Assert.AreEqual(result.Count, 1);
        }
        
        [TestMethod]        
        public void Repo_GetListingByIdTest_InvalidId()
        {           
            //Act
            var result = _customerRepo.GetListingById(null);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Repo_GetListingByCategoryTest()
        {
            // Arrange
            SetUpClassifiedsListing();
            
            //Act
            var result = _customerRepo.GetListingsByCategory("Automobile");

            //Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsNotNull(result[0]);
        }

        [TestMethod]
        public void Repo_GetListingByCategory_InvalidCategory()
        {
            var result = _customerRepo.GetListingsByCategory("qazxsw");
            Assert.AreEqual(0, result.Count);                  
        }
        
        [TestMethod]
        public void Repo_PostListTest()
        {
            //Arrange
            Listing lstObject = GetListObject();
            //_moqAppManager.Setup(x => x.Add(It.IsAny<Listing>())).Returns(lstObject);

            //Act
            var result = _customerRepo.Add(lstObject);

            //Assert
            Assert.IsNotNull(result, null);
            Assert.IsInstanceOfType(result, typeof(Listing));
        }

        [TestMethod]
        public void Repo_PostListTest_EmptyList()
        {
            var result = _customerRepo.Add(null);
            Assert.IsNull(result, null);
        }

        [TestMethod]
        public void Repo_DeleteListTest()
        {
            //Arrange
            Listing lstObject = new Listing
            {
               // _id = Guid.NewGuid().ToString(),
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

            //Act
            var result = _customerRepo.Add(lstObject);
            Assert.IsNotNull(result._id);
            _customerRepo.Delete(result._id);

            //Assert
            Assert.IsTrue(true);
            
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Repo_DeleteListTest_InvalidId()
        {
            _customerRepo.Delete(null);
            Assert.IsTrue(true);           
        }

        [TestMethod]
        public void Repo_PutListTest()
        {
            //Arrange
            Listing lstObject = GetListObject();


            //Act
            var result = _customerRepo.Add(lstObject);
            Assert.IsNotNull(result._id);
            result.Title = "UpdatedTest";
            result.ListingCategory = "UpdatedHousing";
           
            var Updatedresult = _customerRepo.Update(result._id, result);
            Assert.IsNotNull(Updatedresult);
           
            Assert.AreEqual(result.Title, Updatedresult.Title);
            Assert.IsInstanceOfType(result, typeof(Listing));
        }

        [TestMethod]
        public void Repo_PutListTest_EmptyList()
        {
            Listing updatedList = null;
            var result = _customerRepo.Update(null, updatedList);
            Assert.IsNull(result);
        }
        #endregion
      
    }
}
