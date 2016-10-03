using ClassifiedsMongoService.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClassifiedsMongoService.Controllers
{
    public class ClassifiedsController : ApiController
    {
        public MongoClient server = null;
        public IMongoDatabase database = null;

        public ClassifiedsController()
        {
            
        }

        // GET: api/Classifieds
        public List<PersonElasticSerchModel> Get()
        {
            ElasticSearchSample obj = new ElasticSearchSample();
            var result = obj.GetResult();
            return result;

            //Code for retrive data from mongodb
            //MongoClient server = new MongoClient("mongodb://127.0.0.1");            
            //database = server.GetDatabase("test");
            //var collection = database.GetCollection<Person>("testclassified");
            //var filterCondition = Builders<Person>.Filter.Empty;
            //var entity = database.GetCollection<Person>("testclassified").Find<Person>(filterCondition).ToList();
            //return entity.ToList();      


        }

        // GET: api/Classifieds/5
        public List<PersonElasticSerchModel> Get(string textsearch)
        {
            ElasticSearchSample obj = new ElasticSearchSample();
            return obj.GetResult(textsearch);
        }

        // POST: api/Classifieds
        public void Post([FromBody]Person value)
        {
            MongoClient server = new MongoClient("mongodb://127.0.0.1");
            database = server.GetDatabase("test");
            var collection = database.GetCollection<Person>("testclassified");
            value.Id = new ObjectId();
            collection.InsertOne(value);
           
            ElasticSearchSample obj = new ElasticSearchSample();
            PersonElasticSerchModel model = new PersonElasticSerchModel();
            model.Age = value.Age;
            model.FirstName = value.FirstName;
            model.LastName = value.LastName;
            model.mongodbID = value.Id.ToString();

            obj.AddNewIndex(model);            
        }

        // PUT: api/Classifieds/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Classifieds/5
        public void Delete(int id)
        {
        }
    }
    
    public class ElasticSearchSample
    {
        ElasticClient client = null;
        public ElasticSearchSample()
        {
            var uri = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(uri);
            client = new ElasticClient(settings);
            settings.DefaultIndex("testclassified");
        }
        public List<PersonElasticSerchModel> GetResult()
        {   
            if (client.IndexExists("testclassified").Exists)
            {
                var response = client.Search<PersonElasticSerchModel>();
                return response.Documents.ToList();                
            }
            return null;
        }

        public List<PersonElasticSerchModel> GetResult(string condition)
        {
            if (client.IndexExists("testclassified").Exists)
            {
                var query = condition;

                return client.SearchAsync<PersonElasticSerchModel>(s => s
                   .From(0)
                   .Take(10)
                   .Query(qry => qry
                       .Bool(b => b
                       .Must(m => m
                           .QueryString(qs => qs
                               .DefaultField("_all")
                               .Query(query)))))).Result.Documents.ToList();
            }
            return null;
        }

        public void AddNewIndex(PersonElasticSerchModel model)
        {
            client.IndexAsync<PersonElasticSerchModel>(model, null);
        }
    }

    public class Person
    {
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class PersonElasticSerchModel
    {
        public string mongodbID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
