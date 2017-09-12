using System.Collections.Generic;
using System.Web.Http;
using MyWallet.Application.Contracts;

namespace MyWallet.WebApi.Controllers
{
    [RoutePrefix("api/wallet")]
    public class WalletController : ApiController
    {

        private readonly IWalletAppService _walletAppService;

        public WalletController(IWalletAppService walletAppService)
        {
            _walletAppService = walletAppService;
        }

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
