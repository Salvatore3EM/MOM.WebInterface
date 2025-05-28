using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace MOM.WebInterface.Controllers
{
    [RoutePrefix("api/v2")]
    public class SynopticTestController : ApiController
    {
        [HttpPost]
        [Route("GetSynopticData")]
        public HttpResponseMessage GetSynopticData()
        {
            
            var result = new JObject
            {
               
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
