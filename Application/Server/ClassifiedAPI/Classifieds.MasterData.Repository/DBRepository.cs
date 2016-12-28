# region Imports
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Classifieds.MastersData.Repository
{
    public class DBRepository : IDBRepository
    {

        private string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["MasterDataConnectionString"].ConnectionString;
        private string DATABASE = ConfigurationManager.AppSettings["MasterDataDBName"];

        private MongoClient client = null;
        private MongoServer server = null;
        private MongoDatabase db = null;

        public DBRepository()
        {
            client = new MongoClient(CONNECTION_STRING);
            server = client.GetServer();
            db = server.GetDatabase(DATABASE);
        }

        protected MongoDatabase Database
        {
            get { return server.GetDatabase(DATABASE); }
        }

        public MongoCollection<MasterData> GetCollection<MasterData>(string name)
        {
            return db.GetCollection<MasterData>(name);
        }
    }
}
