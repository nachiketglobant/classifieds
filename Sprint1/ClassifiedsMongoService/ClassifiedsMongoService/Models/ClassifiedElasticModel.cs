using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassifiedsMongoService.Models
{
    public class ClassifiedElasticModel
    {
        public string ClassifiedUniqueID { get; set; }
        public int Index { get; set; }
        public string Title { get; set; }
        public string ClassifiedType { get; set; }
        public string ClassifiedSubType { get; set; }
        public string ClassifiedDisciption { get; set; }
        public string status { get; set; }
        public string EntryDate { get; set; }
        public string User { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ContactMobile { get; set; }
    }
}