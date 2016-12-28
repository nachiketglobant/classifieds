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
        /// <summary>
        /// Insert a new MasterData object into the database
        /// </summary>
        /// <param name="object">MasterData object</param>
        /// <returns>return newly added MasterData object</returns>
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

        /// <summary>
        /// Update existing MasterData object based on id from the database
        /// </summary>
        /// <param name="id">MasterData Id</param>
        /// <param name="object">MasterData object </param>
        /// <returns>return updated MasterData object</returns>
        public MasterData Update(string id, MasterData listObj)
        {
            try
            {
                var query = Query<MasterData>.EQ(p => p._id, id);
                var update = Update<MasterData>.Set(p => p.ListingCategory, listObj.ListingCategory);
                var result = this.classifieds.Update(query, update);
                if (result.DocumentsAffected == 0 && result.HasLastErrorMessage)
                {
                    //Trace.TraceError(result.LastErrorMessage);
                }

                return listObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete MasterData object based on id from the database
        /// </summary>
        /// <param name="id">MasterData Id</param>
        /// <returns>return void</returns>
        public void Delete(string id)
        {
            try
            {
                var query = Query<MasterData>.EQ(p => p._id, id.ToString());
                var result = this.classifieds.Remove(query);
                if (result.DocumentsAffected == 0 && result.HasLastErrorMessage)
                {
                    //Trace.TraceError(result.LastErrorMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
