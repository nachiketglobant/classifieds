using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactCardsWebApi.Models
{
    public class StudentModel
    {//[BsonElement("_id")]
        public object _id { get; set; } //MongoDb uses this field as ideantity.
        public string StudentID { get; set; }
        public string RollNo { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }

       
    //"title":"Flat on rent",
    //"description":"2 BHK prime location",
    //"contactperson":"Mr. Abc Xyz",
    //"email":"test@test.com",
    //"mobile":"(555) 555-5555",
    //"category":"Flat",
    //"classifiedtype":"rent",
    //"address":"pune, maharashtra",
    //"city":"pune",
    //"state":"MH",
    //"country":"india",
    //"postedon":"30/09/2016",
    //"isactive":"1"
    }
}