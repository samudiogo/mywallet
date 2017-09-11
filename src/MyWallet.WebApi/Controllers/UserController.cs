using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyWallet.Application.Dto;

namespace MyWallet.WebApi.Controllers
{
    public class UserController : ApiController
    {
        

        // GET: api/User/5
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public void Register([FromBody]UserRegistrationDto user)
        {
        }

        // PUT: api/User/5
        [HttpPost]
        public void Authenticate(int id, [FromBody]string value)
        {
        }
        [HttpDelete]
        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
