using Angular.CSharp.Training.Data;
using Angular.CSharp.Training.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace Angular.CSharp.Training.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private DemoDbContext context = new DemoDbContext();

        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register([FromBody] UserRegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = context.Users.SingleOrDefault(u => u.Email == model.Email);
                if (user != null)
                {
                    return Conflict(); // Email already exists
                }

                // Hash the password before saving
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

                var newUser = new User
                {
                    Email = model.Email,
                    PasswordHash = hashedPassword,
                    Role = model.Role // Set the role
                };

                context.Users.Add(newUser);
                context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] UserLoginModel model)
        {
            try
            {
                var user = context.Users.SingleOrDefault(u => u.Email == model.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                {
                    return Unauthorized();
                }

                // Generate a token and return it along with the user's role
                var token = GenerateToken(user);

                return Ok(new { Token = token, Role = user.Role });
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
            // This is a placeholder. Handle token invalidation here if necessary.
            return Ok();
        }

        private string GenerateToken(User user)
        {
            // Dummy token generation logic: base64 encode email and current timestamp
            var tokenPayload = $"{user.Email}:{DateTime.UtcNow}";
            var tokenBytes = Encoding.UTF8.GetBytes(tokenPayload);
            var token = Convert.ToBase64String(tokenBytes);
            return token;
        }
    }

    public class UserRegistrationModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // New Role property
    }

    public class UserLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
