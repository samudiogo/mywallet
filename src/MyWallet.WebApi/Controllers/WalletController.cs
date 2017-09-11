
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyWallet.WebApi.Controllers
{
    public class WalletController : ApiController
    {
        // GET: api/Wallet
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Wallet/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Wallet
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Wallet/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Wallet/5
        public void Delete(int id)
        {
        }
    }
}
