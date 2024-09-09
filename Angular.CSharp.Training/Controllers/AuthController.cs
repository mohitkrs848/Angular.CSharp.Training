using Angular.CSharp.Training.Data;
using Angular.CSharp.Training.Models;
using Angular.CSharp.Training.Services;
using Google.Apis.Auth;
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
        private readonly DemoDbContext demoDbContext;

        public AuthController(AuthService authService, DemoDbContext demoDbContext)
        {
            this.authService = authService;
            this.demoDbContext = demoDbContext;
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

        [HttpPost]
        [Route("google-login")]
        public async Task<IHttpActionResult> GoogleLogin([FromBody] GoogleLoginModel model)
        {
            try
            {
                // Verify Google token here (use Google's libraries or your logic)
                var payload = await GoogleJsonWebSignature.ValidateAsync(model.Token);
                var userEmail = payload.Email;

                // Check if user exists in the database
                var user = demoDbContext.Users.SingleOrDefault(u => u.Email == userEmail);
                if (user == null)
                {
                    // Optionally, register the user automatically
                    var newUser = new User
                    {
                        Email = userEmail,
                        Role = "User",
                        PasswordHash = null // As they are using Google, no password is required
                    };
                    demoDbContext.Users.Add(newUser);
                    await demoDbContext.SaveChangesAsync();
                    user = newUser;
                }

                var token = AuthService.GenerateToken(user);
                return Ok(new { Token = token, Role = user.Role, LoggedUser = user.Email.Split('@').First() });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
