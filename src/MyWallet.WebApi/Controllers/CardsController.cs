using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using MyWallet.Application.Contracts;
using MyWallet.Application.Dto;
using MyWallet.WebApi.Models;
using static MyWallet.WebApi.Utils.ResponseMessageUtils;
namespace MyWallet.WebApi.Controllers
{
    [RoutePrefix("api/cards")]
    public class CardsController : ApiController
    {
        private readonly ICardAppService _cardAppService;

        public CardsController(ICardAppService cardAppService)
        {
            _cardAppService = cardAppService;
        }

        [HttpGet]
        [Route("{creditCardNumber}")]
        // GET: api/Cards/5
        public IHttpActionResult Get(string creditCardNumber)
        {
            try
            {
                var result = _cardAppService.GetCreditCardByNumber(creditCardNumber);
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
        // POST: api/Cards
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]CardSaveOrUpdateDto cardRegistrationDto)
        {
            if (!ModelState.IsValid) return ResponseMessage(CustomMessage(Request, HttpStatusCode.BadRequest, "Invalid card information"));

            var result = await _cardAppService.RegisterAsync(cardRegistrationDto);
            if (result == null) return ResponseMessage(CustomMessage(Request, HttpStatusCode.BadRequest, "An error occurred while saving this record"));
            return Ok(result);
        }

        // PUT: api/Cards
        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody]CardSaveOrUpdateDto cardDto)
        {
            if (!ModelState.IsValid) return ResponseMessage(CustomMessage(Request, HttpStatusCode.BadRequest, "Invalid card information"));

            var result = await _cardAppService.UpdateAsync(cardDto);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{creditCardNumber}")]
        // DELETE: api/Cards/55555555555555555
        public async Task<IHttpActionResult> Delete(string creditCardNumber)
        {
            try
            {
                await _cardAppService.RemoveCreditCard(creditCardNumber);

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
