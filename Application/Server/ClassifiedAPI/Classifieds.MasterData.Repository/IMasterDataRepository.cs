using System.Collections.Generic;
using Classifieds.MastersData.BusinessEntities;

namespace Classifieds.MastersData.Repository
{
    public interface IMasterDataRepository
    {
        List<MasterData> GetAllCategory();
        MasterData Add(MasterData listObject);
        MasterData Update(string id, MasterData listObject);
        void Delete(string id);
    }
}
