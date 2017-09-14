using System;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Web.Http;
using MyWallet.Application.Contracts;
using MyWallet.Application.Dto;
using static MyWallet.WebApi.Utils.ResponseMessageUtils;

namespace MyWallet.WebApi.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        

        [HttpPost]
        [Route("signUp")]
        public async Task<IHttpActionResult> Register([FromBody]UserSaveOrUpdateDto user)
        {
            if (!ModelState.IsValid)
                return ResponseMessage(CustomMessage(Request, HttpStatusCode.Forbidden, "Invalid information"));
            await _userAppService.RegisterAsync(user);
            return Ok();
        }

        
        [HttpPost]
        [Route("login")]
        public  IHttpActionResult Authenticate([FromBody]UserLoginDto userDto)
        {
            if(!ModelState.IsValid)
            return ResponseMessage(CustomMessage(Request, HttpStatusCode.Forbidden, "Invalid information"));

            try
            {
                var userAuthenticated = _userAppService.Authenticate(userDto);
                return Ok(userAuthenticated);
            }
            catch (AuthenticationException aex)
            {
                return ResponseMessage(CustomMessage(Request, HttpStatusCode.Unauthorized,
                    aex.Message));
            }
            catch (Exception e)
            {
                return ResponseMessage(CustomMessage(Request, HttpStatusCode.BadRequest, e.Message));
            }
            
        }
       
    }
}
