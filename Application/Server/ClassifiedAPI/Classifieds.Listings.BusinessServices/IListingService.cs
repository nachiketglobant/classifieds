using System.Collections.Generic;
using Classifieds.Listings.BusinessEntities;

namespace Classifieds.Listings.BusinessServices
{
    public interface IListingService
    {
        List<Listing> GetListingById(string id);
        List<Listing> GetListingsByCategory(string category);
    }
}
