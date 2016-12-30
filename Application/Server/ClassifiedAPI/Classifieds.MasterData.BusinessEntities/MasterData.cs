#region Imports
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
#endregion

namespace Classifieds.MastersData.BusinessEntities
{
    public class MasterData
    {
        #region Propeties

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } //MongoDb uses this field as identity.
        public string ListingCategory { get; set; }
        [BsonElement("SubCategory")]
        public string[] SubCategory { get; set; }

        #endregion

    }
}
