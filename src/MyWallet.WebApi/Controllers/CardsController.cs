using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using MyWallet.Application.Contracts;
using MyWallet.Application.Dto;
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


        // GET: api/Cards/5
        public IHttpActionResult Get(Guid id)
        {
            return Ok();
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

        // PUT: api/Cards/5
        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody]CardDto cardDto)
        {
            if (!ModelState.IsValid) return ResponseMessage(CustomMessage(Request, HttpStatusCode.BadRequest, "Invalid card information"));

            var result = await _cardAppService.UpdateAsync(cardDto);

            return Ok(result);
        }

        // DELETE: api/Cards/5
        public void Delete(int id)
        {
        }
    }
}
