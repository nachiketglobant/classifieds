using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classifieds.Search.BusinessEntities;
using Classifieds.Search.Repository;

namespace Classifieds.Search.BusinessServices
{
    public class SearchService : ISearchService
    {
        private ISearchRepository _searchRepository;
                
        public SearchService()
        {
            _searchRepository = new SearchRepository();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<Classified> FullTextSearch(string searchText)
        {
            try
            {
                return _searchRepository.FullTextSearch(searchText).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
