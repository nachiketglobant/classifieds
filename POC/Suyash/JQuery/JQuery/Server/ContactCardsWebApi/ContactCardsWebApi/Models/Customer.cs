using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactCardsWebApi.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } //MongoDb uses this field as ideantity.
        public string name { get; set; }
        public string designation { get; set; }
        public string email { get; set; }
        public string birthdate { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public string hobies { get; set; }        
        public string postedon { get; set; }
        public string isactive { get; set; }
        //public Nullable<int> CreatedBy { get; set; }               
    }
}