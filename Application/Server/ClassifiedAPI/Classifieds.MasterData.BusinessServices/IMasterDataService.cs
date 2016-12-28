using System.Collections.Generic;
using Classifieds.MastersData.BusinessEntities;

namespace Classifieds.MastersData.BusinessServices
{
    public interface IMasterDataService
    {
        List<MasterData> GetAllCategory();
        MasterData CreateMasterData(MasterData listObject);
        MasterData UpdateMasterData(string id, MasterData listObject);
        void DeleteMasterdata(string id);
    }
}
