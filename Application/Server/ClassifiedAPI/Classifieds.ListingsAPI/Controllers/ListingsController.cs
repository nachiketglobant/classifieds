using Classifieds.Listings.BusinessEntities;
using Classifieds.Listings.BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Classifieds.ListingsAPI.Controllers
{
    public class ListingsController : ApiController
    {
        private IListingService _listingService;

        public ListingsController(IListingService listingService)
        {
            _listingService = listingService;
        }

        public string Get()
        {
            return "Hi Classifieds";
        }

        public List<Listing> GetListingById(string id)
        {
            try
            {
                return _listingService.GetListingById(id).ToList();
            }
            catch (Exception ex)
            {
                //log exception
                throw ex;
            }

        }

        /// <summary>
        /// Returns the listings for given category
        /// </summary>
        /// <param name="category">listing category</param>
        /// <returns></returns>
        public List<Listing> GetListingsByCategory(string category)
        {
            try
            {
                return _listingService.GetListingsByCategory(category);
            }
            catch (Exception ex)
            {
                //log exception
                throw ex;
            }
        }

    }
}
