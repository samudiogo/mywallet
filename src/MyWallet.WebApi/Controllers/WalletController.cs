using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using MyWallet.Application.Contracts;
using MyWallet.Application.Dto;
using static MyWallet.WebApi.Utils.ResponseMessageUtils;
namespace MyWallet.WebApi.Controllers
{
    [RoutePrefix("api/wallets")]
    public class WalletController : ApiController
    {

        private readonly IWalletAppService _walletAppService;

        public WalletController(IWalletAppService walletAppService)
        {
            _walletAppService = walletAppService;
        }

        // GET: api/Wallet
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var result = await _walletAppService.GetWalletById(id);
            if (result == null)
                return ResponseMessage(CustomMessage(Request, HttpStatusCode.NotFound, "Wallet not found"));

            return Ok(result);
        }


        // POST: api/Wallet
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> RegisterWallet([FromBody]WalletSaveOrUpdateDto walletDto)
        {
            try
            {
                if (!ModelState.IsValid) return ResponseMessage(CustomMessage(Request, HttpStatusCode.Forbidden, "Invalid information"));
                var result = await _walletAppService.CreateWallet(walletDto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return ResponseMessage(CustomMessage(Request, HttpStatusCode.BadRequest, e.Message));
            }
        }

    }
}
