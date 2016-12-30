using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classifieds.MastersData.BusinessEntities;
using Classifieds.MastersData.Repository;


namespace Classifieds.MastersData.BusinessServices
{
    public class MasterDataService : IMasterDataService
    {
        #region MasterDataService

        private IMasterDataRepository _masterDataRepository;

        public MasterDataService(IMasterDataRepository MasterDataRepository)
        {
            _masterDataRepository = MasterDataRepository;
        }

        #endregion

        #region GetAllCategory

        public List<MasterData> GetAllCategory()
        {
            try
            {
                return _masterDataRepository.GetAllCategory().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CreateMasterData
        public MasterData CreateMasterData(MasterData listObject)
        {
            try
            {
                return _masterDataRepository.Add(listObject);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region UpdateMasterData

        public MasterData UpdateMasterData(string id, MasterData listObject)
        {
            try
            {
                return _masterDataRepository.Update(id, listObject);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region DeleteMasterdata

        public void DeleteMasterdata(string id)
        {
            try
            {
                _masterDataRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
