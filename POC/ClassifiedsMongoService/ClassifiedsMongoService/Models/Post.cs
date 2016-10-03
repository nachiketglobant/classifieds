using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassifiedsMongoService.Models
{
    public class Post
    {
        [ScaffoldColumn(false)]
        [BsonId]
        public ObjectId PostId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Date { get; set; }

        [Required]
        public string Title { get; set; }

        [ScaffoldColumn(false)]
        public string Url { get; set; }

        [Required]
        public string Summary { get; set; }

        public string Details { get; set; }

        public int Index { get; set; }

        public string ClassifiedUniqueID { get; set; }
    }
}