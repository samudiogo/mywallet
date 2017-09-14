using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using MyWallet.Application.Contracts;
using MyWallet.Application.Dto;
using MyWallet.WebApi.Models;
using Newtonsoft.Json;
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
                return Content(HttpStatusCode.BadRequest, new ErrorReport
                {
                    Category = "RequestError",
                    ErrorItemCollection = ErrorReport.GetExceptionList((int)HttpStatusCode.BadRequest,
                        new Exception("Wallet not found"))
                });

            return Ok(result);
        }


        // POST: api/Wallet
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> RegisterWallet([FromBody]WalletSaveOrUpdateDto walletDto)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception($"Invalid information. {JsonConvert.SerializeObject(walletDto)}");

                var result = await _walletAppService.CreateWallet(walletDto);

                return Ok(result);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, new ErrorReport
                {
                    Category = "RequestError",
                    ErrorItemCollection = ErrorReport.GetExceptionList((int)HttpStatusCode.BadRequest, e)
                });
            }
        }

        [HttpPost]
        [Route("Purchase")]
        public IHttpActionResult RegisterPurChase([FromBody] AcquisitionSaveOrUpdateDto acquisitionDto)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception($"Invalid information. {JsonConvert.SerializeObject(acquisitionDto)}");


                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, new ErrorReport
                {
                    Category = "RequestError",
                    ErrorItemCollection = ErrorReport.GetExceptionList((int)HttpStatusCode.BadRequest, e)
                });
            }
        }
    }
}
