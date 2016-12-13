using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classifieds.Listings.BusinessEntities;
using Classifieds.Listings.Repository;

namespace Classifieds.Listings.BusinessServices
{
    public class ListingService : IListingService
    {
        private IListingRepository _listingRepository;

        public ListingService(IListingRepository ListingRepository)
        {
            //_listingRepository = new ListingRepository();
            _listingRepository = ListingRepository;
        }

        public List<Listing> GetListingById(string id)
        {
            try
            {
                return _listingRepository.GetListingById(id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<Listing> GetListingsByCategory(string category)
        {
            try
            {
                return _listingRepository.GetListingsByCategory(category);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
