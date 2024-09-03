using System.Linq;
using System.Web.Http;
using Angular.CSharp.Training.Data;
using Angular.CSharp.Training.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System;
using Angular.CSharp.Training.Services;

namespace Angular.CSharp.Training.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetUsers()
        {
            try
            {
                var users = await userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                await userService.UpdateUsers(user);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddUser([FromBody] User user)
        {
            try
            {
                if (await userService.GetUserByEmail(user.Email))
                {
                    return BadRequest("Email already exists.");
                    
                }
                await userService.CreateUser(user);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            try
            {
                await userService.DeleteUser(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
