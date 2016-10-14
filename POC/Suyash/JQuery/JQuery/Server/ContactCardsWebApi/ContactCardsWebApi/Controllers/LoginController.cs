using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.DirectoryServices;
using ClassifiedAPI.Models;


namespace ClassifiedAPI.Controllers
{
    //[RoutePrefix("Classified")]
    public class LoginController : ApiController
    {
        
        [HttpPost]
        //[Route("Login")]
        //[ResponseType(typeof(Post))]
        public bool Login([FromBody]User user)
        {
            using(var DE = new DirectoryEntry("LDAP://globant.com", user.UserName, user.Password, AuthenticationTypes.Secure))
            {
                try
                {
                    DE.RefreshCache(); // This will force credentials validation
                    return true;
                }
                catch (Exception ex)
                {
                    // Validation failed - handle how you want
                    return false;
                }
            }
        }
    }
}
