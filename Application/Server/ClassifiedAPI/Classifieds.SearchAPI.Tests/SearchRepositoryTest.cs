using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Classifieds.Search.BusinessEntities;
using Classifieds.Search.Repository;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Bson;
using System.Text;

namespace Classifieds.SearchAPI.Tests
{
    [TestClass]
    public class SearchRepositoryTest
    {
        private Mock<MongoCollection<T>> GetMockedMongoCollection<T>(string databaseName, string collectionName)
        {
            var message = string.Empty;
            var serverSettings = new MongoServerSettings()
            {
                GuidRepresentation = GuidRepresentation.Standard,
                ReadEncoding = new UTF8Encoding(),
                ReadPreference = ReadPreference.Primary,
                WriteConcern = new WriteConcern(),
                WriteEncoding = new UTF8Encoding()
            };

            var server = new Mock<MongoServer>(serverSettings);
            server.Setup(s => s.Settings).Returns(serverSettings);
            server.Setup(s => s.IsDatabaseNameValid(It.IsAny<string>(), out message)).Returns(true);

            var databaseSettings = new MongoDatabaseSettings()
            {
                GuidRepresentation = GuidRepresentation.Standard,
                ReadEncoding = new UTF8Encoding(),
                ReadPreference = ReadPreference.Primary,
                WriteConcern = new WriteConcern(),
                WriteEncoding = new UTF8Encoding()
            };

            var database = new Mock<MongoDatabase>(server.Object, databaseName, databaseSettings);
            database.Setup(db => db.Settings).Returns(databaseSettings);
            database.Setup(db => db.IsCollectionNameValid(It.IsAny<string>(), out message)).Returns(true);

            var collectionSettings = new MongoCollectionSettings
            {
                AssignIdOnInsert = false,
                GuidRepresentation = GuidRepresentation.Standard,
                ReadEncoding = new UTF8Encoding(),
                ReadPreference = ReadPreference.Primary,
                WriteConcern = new WriteConcern(),
                WriteEncoding = new UTF8Encoding()
            };

            var collection = new Mock<MongoCollection<T>>(database.Object, collectionName, collectionSettings);
            return collection;
        }

        [TestMethod]
        public void TestMethod1()
        {
            var message = string.Empty;

            BsonDocument doc01 = new BsonDocument();
            var collection = new Mock<MongoCollection<BsonDocument>>();

            var data = GetMockedMongoCollection<Classified>("classifiedDB", "classified");
            var mockedCollection = collection.Object.Insert(doc01);
        }
    }
}  
    

