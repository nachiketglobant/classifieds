#region using
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Classifieds.MastersData.Repository;
using Classifieds.MastersData.BusinessEntities;
using System.Collections.Generic;
#endregion

namespace Classifieds.MastersData.Repository.Test
{
    [TestClass]
    public class MasterDataRepositoryTest
    {
        #region Class Variables
        private IMasterDataRepository _masterDataRepo;
        private IDBRepository _dbRepository;
        private List<MasterData> classifiedMasterdata = new List<MasterData>();
        #endregion

        #region Initialize
        [TestInitialize]
        public void Initialize()
        {
            _dbRepository = new DBRepository();
            _masterDataRepo = new MasterDataRepository(_dbRepository);

        }
        #endregion

        #region Setup
        private void SetUpClassifiedsMasterData()
        {
            var lstMasterData = GetMasterDataObject();
            classifiedMasterdata.Add(lstMasterData);
        }

        private MasterData GetMasterDataObject()
        {
            MasterData dataObject = new MasterData
            {
                ListingCategory = "Housing",
                SubCategory = new String[] { "Test1", "Test2", "Test3" }
            };
            return dataObject;
        }
        #endregion

        #region Unit Test Cases
        [TestMethod]
        public void Repo_GetAllCategoryTest()
        {
            // Arrange
            SetUpClassifiedsMasterData();

            //Act
            var result = _masterDataRepo.GetAllCategory();

            //Assert
            Assert.IsNotNull(result[0]);
        }

        [TestMethod]
        public void Repo_AddMasterDataTest()
        {
            //Arrange
            MasterData dataObject = GetMasterDataObject();

            //Act
            var result = _masterDataRepo.Add(dataObject);

            //Assert
            Assert.IsNotNull(result, null);
            Assert.IsInstanceOfType(result, typeof(MasterData));
        }

        [TestMethod]
        public void Repo_DeleteMasterDataTest()
        {
            //Arrange
            MasterData dataObject = new MasterData
            {
                ListingCategory = "test",
                SubCategory = new String[] { "Test1", "Test2", "Test3" }
            };

            //Act
            var result = _masterDataRepo.Add(dataObject);
            Assert.IsNotNull(result._id);
            _masterDataRepo.Delete(result._id);

            //Assert
            Assert.IsTrue(true);

        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Repo_DeleteMasterDataTest_InvalidId()
        {
            _masterDataRepo.Delete("qwer");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Repo_DeleteListTest_NUllId()
        {
            _masterDataRepo.Delete(null);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Repo_UpdateMasterDataTest()
        {
            //Arrange
            MasterData lstObject = GetMasterDataObject();
            //Act
            var result = _masterDataRepo.Add(lstObject);
            Assert.IsNotNull(result._id);
            result.ListingCategory = "UpdatedHousing";

            var Updatedresult = _masterDataRepo.Update(result._id, result);
            Assert.IsNotNull(Updatedresult);

            Assert.AreEqual(result.ListingCategory, Updatedresult.ListingCategory);
            Assert.IsInstanceOfType(result, typeof(MasterData));
        }
        #endregion
    }
}
