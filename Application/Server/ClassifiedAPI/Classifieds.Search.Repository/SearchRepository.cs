using System;
using System.Collections.Generic;
using System.Linq;
using Classifieds.Search.BusinessEntities;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System.Configuration;

namespace Classifieds.Search.Repository
{
    public class SearchRepository : DBRepository, ISearchRepository
    {
        private string _ClassifiedsCollection = ConfigurationManager.AppSettings["Collection"];
        MongoCollection<Classified> classifieds
        {
            get { return Database.GetCollection<Classified>(_ClassifiedsCollection); }
        }

        public SearchRepository()
        {
        }

        public List<Classified> FullTextSearch(string searchText)
        {
            try
            {
                List<Classified> result = new List<Classified>();
                result = this.classifieds.Find(Query.Text(searchText)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
