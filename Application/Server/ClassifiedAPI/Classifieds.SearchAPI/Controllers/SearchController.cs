using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Classifieds.Search.BusinessEntities;
using Classifieds.Search.BusinessServices;

namespace Classifieds.SearchAPI.Controllers
{
    
    public class SearchController : ApiController
    {
        private ISearchService _searchService;

        public SearchController()
        {
            _searchService = new SearchService();
        }

        public string Get()
        {
            return "Hi Classifieds";
        }
       
        public List<Classified> GetFullTextSearch(string searchText)
        {
            try
            {
                return _searchService.FullTextSearch(searchText).ToList();
            }
            catch (Exception ex)
            {
                //log exception
                throw ex;
            }
          
        }
    }
}
