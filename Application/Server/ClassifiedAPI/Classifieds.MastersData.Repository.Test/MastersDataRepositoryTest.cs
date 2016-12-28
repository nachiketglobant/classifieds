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
    public class MastersDataRepositoryTest
    {
        #region Class Variables
        private IMasterDataRepository _masterDataRepo;
        private IDBRepository _dbRepository;
        private List<MasterData> classifiedData = new List<MasterData>();
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
            var lstMasterData = GetMatserDataObject();
            classifiedData.Add(lstMasterData);
        }

        private MasterData GetMatserDataObject()
        {
            MasterData dataObject = new MasterData
            {
                ListingCategory = "Housing",
                //SubCategory = ["Car", "Bike", "Plan"]
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

            //GetAllCategory
            var result = _masterDataRepo.GetAllCategory();

            //Assert
            //Assert.AreEqual(1, result.Count);
            Assert.IsNotNull(result[0]);
        }

        [TestMethod]
        public void Repo_GetAllCategory_Invalid_OR_Null_Category()
        {
            var result = _masterDataRepo.GetAllCategory();
            Assert.AreEqual(0, result.Count);

            //var nullresult = _masterDataRepo.GetAllCategory();
            //Assert.AreEqual(0, nullresult.Count);
        }

        [TestMethod]
        public void Repo_GetAllCategoryTest_Invalid()
        {
            var result = _masterDataRepo.GetAllCategory();
            Assert.IsNull(result);
        }


        [TestMethod]
        public void Repo_AddMasterDataTest()
        {
            //Arrange
            MasterData dataObject = GetMatserDataObject();
            //_moqAppManager.Setup(x => x.Add(It.IsAny<MasterData>())).Returns(dataObject);

            //Act
            var result = _masterDataRepo.Add(dataObject);

            //Assert
            Assert.IsNotNull(result, null);
            Assert.IsInstanceOfType(result, typeof(MasterData));
        }

        [TestMethod]
        public void Repo_AddListTest_EmptyList()
        {
            var result = _masterDataRepo.Add(null);
            Assert.IsNull(result, null);
        }

        [TestMethod]
        public void Repo_DeleteMasterDataTest()
        {
            //Arrange
            MasterData dataObject = new MasterData
            {
                // _id = Guid.NewGuid().ToString(),

                ListingCategory = "test",
                //SubCategory = { "Bike", "Car", "Plane"},
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
        public void Repo_DeleteMasterDataTest_NUllId()
        {
            _masterDataRepo.Delete(null);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Repo_UpdateMasterDataTest()
        {
            //Arrange
            MasterData dataObject = GetMatserDataObject();


            //Act
            var result = _masterDataRepo.Add(dataObject);
            Assert.IsNotNull(result._id);
            result.ListingCategory = "UpdatedHousing";

            var Updatedresult = _masterDataRepo.Update(result._id, result);
            Assert.IsNotNull(Updatedresult);

            Assert.AreEqual(result.ListingCategory, Updatedresult.ListingCategory);
            Assert.IsInstanceOfType(result, typeof(MasterData));
        }

        [TestMethod]
        public void Repo_UpdateMasterDataTest_EmptyList()
        {
            MasterData updatedData = null;
            var result = _masterDataRepo.Update(null, updatedData);
            Assert.IsNull(result);
        }


        [TestMethod]
        public void Repo_GetAllCategoryTest_NullSubCategory()
        {
            var result = _masterDataRepo.GetAllCategory();
            Assert.IsTrue(true);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Repo_GetAllCategoryTest_InvalidSubCategory()
        {
            var result = _masterDataRepo.GetAllCategory();
            Assert.IsNull(result);
        }

        #endregion
    }
}
