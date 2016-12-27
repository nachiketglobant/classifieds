using Classifieds.Common;
using Classifieds.Listings.BusinessEntities;
using Classifieds.Listings.BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Classifieds.ListingsAPI.Controllers
{
    public class ListingsController : ApiController
    {
        #region Private Variable
        private IListingService _listingService;
        private ILogger _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// The class constructor. </summary>
        public ListingsController(IListingService listingService, ILogger logger)
        {
            _listingService = listingService;
            _logger = logger;
        }
        #endregion

       /// <summary>
        /// Returns the listing for given id
        /// </summary>
        /// <param name="id">listing id</param>
        /// <returns></returns>
        public List<Listing> GetListingById(string id)
        {
            try
            {
                return _listingService.GetListingById(id).ToList();
            }
            catch (Exception ex)
            {
                //log exception
                throw _logger.Log(ex, "Globant/User");
            }

        }

        /// <summary>
        /// Returns the listings for given sub category
        /// </summary>
        /// <param name="subCategory">listing Sub Category</param>
        /// <returns></returns>
        public List<Listing> GetListingsBySubCategory(string subCategory)
        {
            try
            {
                return _listingService.GetListingsBySubCategory(subCategory).ToList();
            }
            catch (Exception ex)
            {
                //log exception
                throw _logger.Log(ex, "Globant/User");
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
                return _listingService.GetListingsByCategory(category).ToList();
            }
            catch (Exception ex)
            {
                //log exception
                throw _logger.Log(ex, "Globant/User");
            }
        }

        public HttpResponseMessage Post(Listing listingObj)
        {
            HttpResponseMessage result = null;
            try
            {
                var Classified = _listingService.CreateListing(listingObj);
                result = Request.CreateResponse<Listing>(HttpStatusCode.Created, Classified);
                string newItemURL = Url.Link("Listings", new { id = Classified._id });
                result.Headers.Location = new Uri(newItemURL);
            }
            catch (Exception ex)
            {
                result = Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message);
                //log exception
                throw _logger.Log(ex, "Globant/User");
            }
            return result;
        }

        public HttpResponseMessage Put(string id, Listing value)
        {
            HttpResponseMessage result = null;

            try
            {
                var Classified = _listingService.UpdateListing(id, value);
                result = Request.CreateResponse<Listing>(HttpStatusCode.Accepted, Classified);
            }
            catch (Exception ex)
            {
                result = Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message);
                //log exception
                throw _logger.Log(ex, "Globant/User");
            }

            return result;
        }

        public HttpResponseMessage Delete(string id)
        {
            HttpResponseMessage result = null;

            try
            {
                _listingService.DeleteListing(id);
                result = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                result = Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message);
                //log exception
                throw _logger.Log(ex, "Globant/User");
            }

            return result;
        }

        private HttpResponseMessage GetBadRequestResponse()
        {
            HttpResponseMessage response = null;
            List<string> errors = new List<string>();
            foreach (var modelSt in ModelState.Values)
            {
                foreach (var error in modelSt.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }

            response = Request.CreateResponse<IEnumerable<string>>(HttpStatusCode.BadRequest, errors);
            return response;
        }

        public List<Listing> GetTopListings(int noOfRecords=10)
        {
            try
            {
                return _listingService.GetTopListings(noOfRecords);
            }
            catch (Exception ex)
            {
                //log exception
                throw _logger.Log(ex, "Globant/User");
            }
        }
    }
}
