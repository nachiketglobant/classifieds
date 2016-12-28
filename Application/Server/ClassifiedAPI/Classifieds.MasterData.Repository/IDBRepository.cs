using MongoDB.Driver;

namespace Classifieds.MastersData.Repository
{
    public interface IDBRepository
    {
        MongoCollection<MasterData> GetCollection<MasterData>(string name);
    }
}
