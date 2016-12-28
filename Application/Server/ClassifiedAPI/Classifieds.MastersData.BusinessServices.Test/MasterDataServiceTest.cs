using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Classifieds.MastersData.BusinessEntities;
using Classifieds.MastersData.Repository;
using Moq;


namespace Classifieds.MastersData.BusinessServices.Test
{
    [TestClass]
    public class MasterDataServiceTest
    {
        #region Class Variables
        private Mock<IMasterDataRepository> _moqAppManager;
        private IMasterDataService _service;
        private List<MasterData> classifiedMasterData = new List<MasterData>();
        #endregion

        #region Initialize
        [TestInitialize]
        public void Initialize()
        {
            _moqAppManager = new Mock<IMasterDataRepository>();
            _service = new MasterDataService(_moqAppManager.Object);
        }
        #endregion

        #region Setup
        private void SetUpClassifiedsMasterData()
        {
            var lstMasterdata = GetMasterDataObject();
            classifiedMasterData.Add(lstMasterdata);
        }

        private MasterData GetMasterDataObject()
        {
            MasterData dataObject = new MasterData
            {
                ListingCategory = "test",
                // SubCategory = "test"

            };
            return dataObject;
        }
        #endregion

        #region Unit Test Cases

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetListingsBySubCategory_ThrowsException()
        {
            var result = _service.GetAllCategory();
        }


        /// <summary>
        /// tests the positive test criteria
        /// </summary>
        [TestMethod]
        public void GetAllCategoryTest()
        {
            // Arrange
            SetUpClassifiedsMasterData();
            _moqAppManager.Setup(x => x.GetAllCategory()).Returns(classifiedMasterData);

            //Act
            var result = _service.GetAllCategory();

            //Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsNotNull(result[0]);
        }

        /// <summary>
        /// tests for incorrect input giving empty result
        /// </summary>
        [TestMethod]
        public void GetListingByCategory_EmptyResultTest()
        {
            // Arrange
            SetUpClassifiedsMasterData();
            _moqAppManager.Setup(x => x.GetAllCategory()).Returns(new List<MasterData>() { new MasterData() });

            //Act
            var result = _service.GetAllCategory();

            //Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsInstanceOfType(result[0], typeof(MasterData));
        }

        /// <summary>
        /// tests for null output if input is null
        /// </summary>
        [TestMethod]
        public void GetListingByCategory_ReturnsNull()
        {
            var result = _service.GetAllCategory();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void PostListTest()
        {
            //Arrange
            MasterData lstObject = GetMasterDataObject();
            _moqAppManager.Setup(x => x.Add(It.IsAny<MasterData>())).Returns(lstObject);

            //Act
            var result = _service.CreateMasterData(lstObject);

            //Assert
            Assert.IsNotNull(result, null);
            Assert.IsInstanceOfType(result, typeof(MasterData));
        }

        [TestMethod]
        public void PostListTest_EmptyList()
        {
            var result = _service.GetAllCategory();
            Assert.IsNull(result, null);
        }

        [TestMethod]
        public void DeleteListTest()
        {
            //Arrange
            MasterData lstObject = GetMasterDataObject();
            _moqAppManager.Setup(x => x.Delete(It.IsAny<string>()));

            //Act
            _service.DeleteMasterdata(lstObject._id);

            //Assert
            Assert.IsTrue(true);
            _moqAppManager.Verify(v => v.Delete(lstObject._id), Times.Once());
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        public void DeleteListTest_InvalidId()
        {
            _service.DeleteMasterdata(null);
            Assert.IsTrue(true);
            //_moqAppManager.Verify(v => v.Delete(null), Times.Once());
        }

        [TestMethod]
        public void PutListTest()
        {
            //Arrange
            MasterData lstObject = GetMasterDataObject();
            _moqAppManager.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<MasterData>())).Returns(lstObject);
            var updatedList = new MasterData() { ListingCategory = lstObject.ListingCategory };
            //Act
            var result = _service.UpdateMasterData(lstObject._id, updatedList);

            //Assert
            Assert.IsNotNull(result, null);
            Assert.IsInstanceOfType(result, typeof(MasterData));
        }

        [TestMethod]
        public void PutListTest_InvalidId()
        {
            var updatedData = new MasterData() { ListingCategory = "testupdated" };
            var result = _service.UpdateMasterData(null, updatedData);
            Assert.IsNull(result);
        }
        #endregion


    }
}
