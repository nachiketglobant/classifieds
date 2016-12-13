using Classifieds.Listings.BusinessEntities;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Classifieds.Listings.Repository
{
    public class ListingRepository : IListingRepository
    {

        private const string CONNECTION_STRING = "mongodb://localhost";
        private const string DATABASE = "classifiedDB";//"Classifieds";
        private const string COLLECTION_Classifieds = "Listings";

        private MongoClient client = null;
        private MongoServer server = null;
        private MongoDatabase db = null;
        private MongoCollection<Listing> classifieds = null;

        public ListingRepository()
        {
            client = new MongoClient(CONNECTION_STRING);
            server = client.GetServer();
            db = server.GetDatabase(DATABASE);
            classifieds = db.GetCollection<Listing>(COLLECTION_Classifieds);
        }
        public List<Listing> GetListingById(string id)
        {
            try
            {
                var partialRresult = this.classifieds.FindAll() 
                                        .Where(p => p._id == id)
                                        .ToList();

                List<Listing> result = partialRresult.Count > 0 ? partialRresult.ToList() : null;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns a collection of listings based on category
        /// </summary>
        /// <param name="category">listing category</param>
        /// <returns>Collection of listings</returns>
        public List<Listing> GetListingsByCategory(string category)
        {
            try
            {
                List<Listing> result = this.classifieds.FindAll()
                                            .Where(p => p.ListingCategory == category)
                                            .ToList();
                return result;
            }
            catch (MongoException ex)
            {
                return null;
            }
        }
    }
}
