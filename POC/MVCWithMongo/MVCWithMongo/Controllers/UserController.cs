using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVCWithMongo.Models;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MVCWithMongo.Controllers
{
    public class UserController : Controller
    {
        
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserModel um)
        {
            //Connect to MongoDB
            MongoClient client = new MongoClient("Server=localhost:27017");
            MongoServer objServer = client.GetServer();
            MongoDatabase objDatabse = objServer.GetDatabase("MVCTestDB");
        
            MongoCollection<BsonDocument> UserDetails = objDatabse.GetCollection<BsonDocument>("Users");
            //Insert into Users table.
            BsonDocument objDocument = new BsonDocument {
                {"ID",um.ID},
                {"UserName",um.UserName},
                {"Password",um.Password},
                {"Email",um.Email},
                 {"PhoneNo",um.PhoneNo},
                {"Address",um.Address}
                };

            UserDetails.Insert(objDocument);
            return RedirectToAction("GetUsers");
        }

        public ActionResult GetUsers()
        {
            MongoClient client = new MongoClient("Server=localhost:27017");
            MongoServer objServer = client.GetServer();
            MongoDatabase objDatabse = objServer.GetDatabase("MVCTestDB");
            List<UserModel> UserDetails = objDatabse.GetCollection<UserModel>("Users").FindAll().ToList();
            return View(UserDetails);
        }

        public ActionResult Delete(int id)
        {
            MongoClient client = new MongoClient("Server=localhost:27017");
            MongoServer objServer = client.GetServer();
            MongoDatabase objDatabse = objServer.GetDatabase("MVCTestDB");
            IMongoQuery query = Query.EQ("ID",id);
            objDatabse.GetCollection<UserModel>("Users").Remove(query);
            return RedirectToAction("GetUsers");
        }

        public ActionResult Edit(int id)
        {
            MongoClient client = new MongoClient("Server=localhost:27017");
            MongoServer objServer = client.GetServer();
            MongoDatabase objDatabse = objServer.GetDatabase("MVCTestDB");
            IMongoQuery query = Query.EQ("ID", id);
            UserModel user = objDatabse.GetCollection<UserModel>("Users").Find(query).SingleOrDefault();
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserModel um)
        {
            MongoClient client = new MongoClient("Server=localhost:27017");
            MongoServer objServer = client.GetServer();
            MongoDatabase objDatabse = objServer.GetDatabase("MVCTestDB");
            IMongoQuery query = Query.EQ("ID", um.ID);

            IMongoUpdate  updateQuery = Update.Set("UserName", um.UserName).Set("Password", um.Password).Set("Email", um.Email).Set("PhoneNo", um.PhoneNo).Set("Address", um.Address);
            objDatabse.GetCollection<UserModel>("Users").Update(query, updateQuery);
            return RedirectToAction("GetUsers");
        }


    }
}
