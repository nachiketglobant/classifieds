using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Classifieds.Search.BusinessEntities
{
    public class Classified
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } //MongoDb uses this field as ideantity.
        public string title { get; set; }
        public string description { get; set; }
        public string contactperson { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string category { get; set; }
        public string classifiedtype { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postedon { get; set; }
        public int isactive { get; set; }
        //////public Nullable<int> CreatedBy { get; set; }               
    }
}
