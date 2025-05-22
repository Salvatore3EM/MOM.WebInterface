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
            var trim1 = new JArray
            {
                new JObject
                {
                    { "tableid", 1 },
                    { "cis", "6047215" },
                    { "modello", "686" },
                    { "telaio", "PT10125" },
                    { "anomalie", new JArray
                        {
                            new JObject { { "descrizione", "Errore temperatura" } },
                            new JObject { { "descrizione", "Sensore inattivo" } }
                        }
                    }
                },
                new JObject
                {
                    { "tableid", 2 },
                    { "cis", "6043611" },
                    { "modello", "686" },
                    { "telaio", "PT10124" },
                    { "anomalie", new JArray() }
                },
                new JObject
                {
                    { "tableid", 3 },
                    { "cis", "6037211" },
                    { "modello", "686" },
                    { "telaio", "PT10123" },
                    { "anomalie", new JArray() }
                }
            };

            var trim2 = new JArray
            {
                new JObject
                {
                    { "tableid", 4 },
                    { "cis", "6139999" },
                    { "modello", "686" },
                    { "telaio", "PT10106" },
                    { "anomalie", new JArray
                        {
                            new JObject { { "descrizione", "Allarme pressione" } }
                        }
                    }
                }
            };

            var result = new JObject
            {
                { "trim1", trim1 },
                { "trim2", trim2 }
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
