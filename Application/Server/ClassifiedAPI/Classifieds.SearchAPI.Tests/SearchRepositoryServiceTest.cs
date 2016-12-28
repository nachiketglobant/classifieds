﻿using Classifieds.Search.BusinessEntities;
using Classifieds.Search.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifieds.SearchAPI.Tests
{
    [TestClass]
   public class SearchRepositoryServiceTest
    {
        #region Private Variables        
        private ISearchRepository _searchRepo;
        //private IDBRepository _dbRepository;
        #endregion

        #region Initialization
        [TestInitialize]
        public void Initialize()
        { 
            _searchRepo = new SearchRepository();
        }
        #endregion

        #region Private Methods
        private void SetUpClassifields()
        {
            var classified = new Classified()
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

            var classifiedList = new List<Classified>();
            classifiedList.Add(classified);
            //_moqAppManager.Setup(x => x.FullTextSearch(It.IsAny<string>())).Returns(classifiedList);

        }
        #endregion

        #region Test Methods
        [TestMethod]
        public void Repo_FreeTextSearch_Test()
        {
            //Arrange
            SetUpClassifields();
            //Act
            var classifieds = _searchRepo.FullTextSearch("Automobile");
            //Assert
            Assert.AreEqual(classifieds.Count, 1);

        }

        [TestMethod]
        public void Repo_FreeTextSearch_EmptyText_Test()
        {
            //Arrange
            string searchText = string.Empty;
            //Act
            var result = _searchRepo.FullTextSearch(searchText);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 0);
            Assert.IsInstanceOfType(result, typeof(IList<Classified>));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Repo_FreeTextSearch_ThrowsException()
        {
            var result = _searchRepo.FullTextSearch(null);
        }
        #endregion
    }
}