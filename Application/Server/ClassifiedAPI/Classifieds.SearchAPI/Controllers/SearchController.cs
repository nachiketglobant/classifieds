﻿#region Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Classifieds.Search.BusinessEntities;
using Classifieds.Search.BusinessServices;
using Classifieds.Common;
#endregion  

namespace Classifieds.SearchAPI.Controllers
{
    /// <summary>
    /// This Service is used for Global Search in all categorys
    /// class name: SearchController
    /// Purpose : This class is used for Global Search in all categorys.
    /// Created By : Amol Pawar
    /// Created Date: 08/12/2016
    /// Modified by :
    /// Modified date:
    /// </summary>
    public class SearchController : ApiController
    {
        #region Private Variable
        private ISearchService _searchService;
        private ILogger _logger;
        #endregion 

        #region Constructor
        /// <summary>
        /// The class constructor. </summary>
        public SearchController(ISearchService searchService,ILogger logger)
        {
            _searchService = searchService;
            _logger = logger;
        }
        #endregion

        #region Public_Methods
        /// <summary>
        /// GetFulltext search on title, description and category
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns>SearchResult</returns>
        public List<Classified> GetFullTextSearch(string searchText)
        {
            try
            {
                return _searchService.FullTextSearch(searchText).ToList();
                //throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                //ToDo seName is hardcoded
                throw _logger.Log(ex,"Globant/User");
            }

        }
        #endregion
    }
}