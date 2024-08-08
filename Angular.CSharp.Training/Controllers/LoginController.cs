using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Angular.CSharp.Training.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(Login loginModel)
        {
            if (loginModel.Username == "admin" && loginModel.Password == "password")
            {
                // Return a success response
                return Ok();
            }
            return Unauthorized();
        }
    }
}