using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyWallet.WebApi.Controllers
{
    public class DefaultController : ApiController
    {
        public IHttpActionResult Get(string uri) => ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotFound));
    }
}
