using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classifieds.Search.BusinessEntities;

namespace Classifieds.Search.BusinessServices
{
    public interface ISearchService
    {
        List<Classified> FullTextSearch(string searchText);
    }
}
