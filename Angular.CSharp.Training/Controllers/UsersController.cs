using System.Linq;
using System.Web.Http;
using Angular.CSharp.Training.Data;
using Angular.CSharp.Training.Models;
using System.Data.Entity;

namespace Angular.CSharp.Training.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private DemoDbContext _context = new DemoDbContext();

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult UpdateUser(int id, [FromBody] User user)
        {
            var existingUser = _context.Users.Find(id);
            if (existingUser == null) return NotFound();

            existingUser.Email = user.Email;
            existingUser.Role = user.Role;
            _context.SaveChanges();

            return Ok(existingUser);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddUser([FromBody] User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok();
        }

    }
}
