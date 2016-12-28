using Classifieds.MastersData.BusinessEntities;
using Classifieds.MastersData.BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Classifieds.Common;

namespace Classifieds.MasterDataAPI.Controllers
{
    public class MastersDataController : ApiController
    {
        #region Private Variable

        private IMasterDataService _masterDataService;
        private ILogger _logger;

        #endregion

        public MastersDataController(IMasterDataService masterdataService, ILogger logger)
        {
            _masterDataService = masterdataService;
            _logger = logger;
        }

        [HttpGet]
        public List<MasterData> GetAllCategory()
        {
            try
            {
                return _masterDataService.GetAllCategory().ToList();
                //throw new ArgumentException();
            }
            catch (Exception ex)
            {
                //log exception
                throw _logger.Log(ex, "Globant/User");
            }
        }

        [HttpPost]
        public HttpResponseMessage Post(MasterData masterDataObj)
        {
            HttpResponseMessage result = null;

            if (ModelState.IsValid)
            {
                try
                {
                    var Classified = _masterDataService.CreateMasterData(masterDataObj);
                    result = Request.CreateResponse<MasterData>(HttpStatusCode.Created, Classified);
                    string newItemURL = Url.Link("MasterData", new { id = Classified._id });
                    result.Headers.Location = new Uri(newItemURL);
                }
                catch (Exception ex)
                {
                    result = Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            else
            {
                result = GetBadRequestResponse();
            }

            return result;
        }

        public HttpResponseMessage Put(string id, MasterData value)
        {
            HttpResponseMessage result = null;

            if (ModelState.IsValid)
            {
                try
                {
                    var Classified = _masterDataService.UpdateMasterData(id, value);
                    result = Request.CreateResponse<MasterData>(HttpStatusCode.Accepted, Classified);
                    //result = Request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception ex)
                {
                    //Trace.TraceError(ex.Message, ex);
                    result = Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            else
            {
                result = GetBadRequestResponse();
            }

            return result;
        }

        public HttpResponseMessage Delete(string id)
        {
            HttpResponseMessage result = null;

            try
            {
                _masterDataService.DeleteMasterdata(id);
                result = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                //Trace.TraceError(ex.Message, ex);
                result = Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message);
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
    }
}
