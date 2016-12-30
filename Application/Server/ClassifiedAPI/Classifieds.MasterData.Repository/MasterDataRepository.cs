using Classifieds.MastersData.BusinessEntities;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Classifieds.MastersData.Repository
{
    public class MasterDataRepository : DBRepository, IMasterDataRepository
    {
        #region MasterDataRepository
        private string COLLECTION_Classifieds = ConfigurationManager.AppSettings["MasterDataCollection"];

        private IDBRepository _dbRepository;
        public MasterDataRepository(IDBRepository DBRepository)
        {
            _dbRepository = DBRepository;
        }
        MongoCollection<MasterData> classifieds
        {
            get { return _dbRepository.GetCollection<MasterData>(COLLECTION_Classifieds); }
        }

        #endregion

        #region GetAllCategory
        public List<MasterData> GetAllCategory()
        {
            try
            {
                var partialRresult = this.classifieds.FindAll()
                                    .ToList();
                List<MasterData> result = partialRresult.Count > 0 ? partialRresult.ToList() : null;


                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region AddCategory

        /// <summary>
        /// Insert a new Category object into the database
        /// </summary>
        /// <param name="object">Category object</param>
        /// <returns>return newly added Category object</returns>
        public MasterData Add(MasterData masterData)
        {
            try
            {
                var result = this.classifieds.Save(masterData);
                if (result.DocumentsAffected == 0 && result.HasLastErrorMessage)
                {
                    //Trace.TraceError(result.LastErrorMessage);    
                }
                return masterData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Updatecategory

        /// <summary>
        /// Update existing category object based on id from the database
        /// </summary>
        /// <param name="object">category object </param>
        /// <returns>return updated category object</returns>
        /// 

        public MasterData Update(string id, MasterData dataObj)
        {
            try
            {
                var query = Query<MasterData>.EQ(p => p._id, id);
                var update = Update<MasterData>.Set(p => p.ListingCategory, dataObj.ListingCategory)
                                               .Set(p => p.SubCategory, dataObj.SubCategory);
                var result = this.classifieds.Update(query, update);
                if (result.DocumentsAffected == 0 && result.HasLastErrorMessage)
                {

                }

                return dataObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region DeleteCategory
        /// <summary>
        /// Delete category object based on id from the database
        /// </summary>
        /// <param name="id">category Id</param>
        /// <returns>return void</returns>
        public void Delete(string id)
        {
            try
            {
                var query = Query<MasterData>.EQ(p => p._id, id.ToString());
                var result = this.classifieds.Remove(query);
                if (result.DocumentsAffected == 0 && result.HasLastErrorMessage)
                {
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
