using ContactCardsWebApi.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ContactCardsWebApi.Repositories
{
    public class ContactsRepository
    {
        //private const string CONNECTION_STRING = "mongodb://localhost";
        //private const string DATABASE = "contact_db";
        //private const string COLLECTION_CONTACTS = "contacts";

        //private MongoClient client = null;
        //private MongoServer server = null;
        //private MongoDatabase db = null;
        //private MongoCollection<Contact> contacts = null;

        private const string CONNECTION_STRING = "Server=localhost:27017";
        private const string DATABASE = "CustomerDB";
        private const string COLLECTION_CONTACTS = "customer";

        private MongoClient client = null;
        private MongoServer server = null;
        private MongoDatabase db = null;
        private MongoCollection<Customer> contacts = null;

        public ContactsRepository()
        {
            client = new MongoClient(CONNECTION_STRING);
            server = client.GetServer();
            db = server.GetDatabase(DATABASE);
            //contacts = db.GetCollection<Contact>(COLLECTION_CONTACTS);
            contacts = db.GetCollection<Customer>(COLLECTION_CONTACTS);
        }

        public IEnumerable<Customer> GetAll()
        {
            List<Customer> result = new List<Customer>();
            result = this.contacts.FindAll().ToList();
            return result;
        }

        //public IEnumerable<Contact> GetAll()
        //{
        //    List<Contact> result = new List<Contact>();
        //    result = this.contacts.FindAll().ToList();
        //    return result;
        //}

        public Customer Get(string id)
        {
            Customer result = null;
            var partialResult = this.contacts.AsQueryable<Customer>()
                                    .Where(p => p._id == id)
                                    .ToList();

            result = partialResult.Count > 0
                            ? partialResult[0]
                            : null;

            return result;
        }

        public Customer Save(Customer c)
        {
            var result = this.contacts.Save(c);
            if (result.DocumentsAffected == 0 && result.HasLastErrorMessage)
            {
                Trace.TraceError(result.LastErrorMessage);
            }

            return c;
        }

        public Customer Update(string id, Customer c)
        {
            var query = Query<Customer>.EQ(p => p._id, id);
            var update = Update<Customer>.Set(p => p.name, c.name)
                                        .Set(p => p.designation, c.designation)
                                        .Set(p => p.email, c.email)
                                        .Set(p => p.address, c.address)
                                        .Set(p => p.birthdate, c.birthdate)
                                        .Set(p => p.gender, c.gender)
                                        .Set(p => p.hobies, c.hobies);
                                        

            var result = this.contacts.Update(query, update);
            if (result.DocumentsAffected == 0 && result.HasLastErrorMessage)
            {
                Trace.TraceError(result.LastErrorMessage);
            }

            return c;
        }

        public void Delete(string id)
        {
            var query = Query<Customer>.EQ(p => p._id, id.ToString());
            var result = this.contacts.Remove(query);
            if (result.DocumentsAffected == 0 && result.HasLastErrorMessage)
            {
                Trace.TraceError(result.LastErrorMessage);
            }
        }

    }
}