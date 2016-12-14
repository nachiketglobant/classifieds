using Classifieds.Listings.BusinessEntities;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Classifieds.Listings.Repository
{
    public class ListingRepository : DBRepository,IListingRepository
    {
        //private const string COLLECTION_Classifieds = "Listing";
        private string COLLECTION_Classifieds = ConfigurationManager.AppSettings["ListingCollection"];
        MongoCollection<Listing> classifieds
        {
            get { return Database.GetCollection<Listing>(COLLECTION_Classifieds); }
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


        public Listing Add(Listing listing)
        {
            try
            {
                var result = this.classifieds.Save(listing);
                if (result.DocumentsAffected == 0 && result.HasLastErrorMessage)
                {
                    //Trace.TraceError(result.LastErrorMessage);    
                }
                return listing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Listing Update(string id, Listing listObj)
        {
            try
            {
                var query = Query<Listing>.EQ(p => p._id, id);
                var update = Update<Listing>.Set(p => p.Title, listObj.Title)
                                             .Set(p => p.ListingType, listObj.ListingType)
                                             .Set(p => p.ListingCategory, listObj.ListingCategory)
                                             .Set(p => p.Brand, listObj.Brand)
                                             .Set(p => p.Price, listObj.Price)
                                             .Set(p => p.YearOfPurchase, listObj.YearOfPurchase)
                                             .Set(p => p.ExpiryDate, listObj.ExpiryDate)
                                             .Set(p => p.Status, listObj.Status)
                                             .Set(p => p.Submittedby, listObj.Submittedby)
                                             .Set(p => p.SubmittedDate, listObj.SubmittedDate)
                                             .Set(p => p.IdealFor, listObj.IdealFor)
                                             .Set(p => p.Furnished, listObj.Furnished)
                                             .Set(p => p.FuelType, listObj.FuelType)
                                             .Set(p => p.KmDriven, listObj.KmDriven)
                                             .Set(p => p.YearofMake, listObj.YearofMake)
                                             .Set(p => p.Dimensions, listObj.Dimensions)
                                             .Set(p => p.TypeofUse, listObj.TypeofUse)
                                             .Set(p => p.Photos, listObj.Photos)
                                             .Set(p => p.Address, listObj.Address)
                                             .Set(p => p.ContactName, listObj.ContactName)
                                             .Set(p => p.ContactNo, listObj.ContactNo)
                                             .Set(p => p.Details, listObj.Details)
                                             .Set(p => p.Configuration, listObj.Configuration);


                var result = this.classifieds.Update(query, update);
                if (result.DocumentsAffected == 0 && result.HasLastErrorMessage)
                {
                    //Trace.TraceError(result.LastErrorMessage);
                }

                return listObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(string id)
        {
            var query = Query<Listing>.EQ(p => p._id, id.ToString());
            var result = this.classifieds.Remove(query);
            if (result.DocumentsAffected == 0 && result.HasLastErrorMessage)
            {
                //Trace.TraceError(result.LastErrorMessage);
            }
        }
    }
}
