using Angular.CSharp.Training.Data;
using Angular.CSharp.Training.Models;
using Angular.CSharp.Training.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.ApplicationServices;
using System.Web.Http;

namespace Angular.CSharp.Training.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> Register([FromBody] UserRegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await authService.Register(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> Login([FromBody] UserLoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                List<string> result = await authService.Login(model);

                return Ok(new { Token = result[0], Role = result[1], LoggedUser = result[2] });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            return Ok();
        }
    }
}
