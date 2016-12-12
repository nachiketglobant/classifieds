using System.Collections.Generic;
using Classifieds.Listings.BusinessEntities;

namespace Classifieds.Listings.Repository
{
    public interface IListingRepository
    {
        List<Listing> GetListingById(string id);
    }
}
