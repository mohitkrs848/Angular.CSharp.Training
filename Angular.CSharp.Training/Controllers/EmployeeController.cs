using Angular.CSharp.Training.Agents;
using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Angular.CSharp.Training.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private EmployeeAgent employeeAgent;

        public EmployeeController(EmployeeAgent employeeAgent)
        {
            this.employeeAgent = employeeAgent;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            employeeAgent.CreateEmployee(employee);
            return Ok(employee);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetEmployeeById(int id)
        {
            var employee = employeeAgent.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Employee> GetAllEmployees()
        {
            return employeeAgent.GetAllEmployees();
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != employee.Id)
            {
                return BadRequest();
            }
            employeeAgent.UpdateEmployee(employee);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteEmployee(int id)
        {
            var employee = employeeAgent.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            employeeAgent.DeleteEmployee(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IHttpActionResult SearchEmployees([FromUri] string email = null, [FromUri] int? employeeId = null)
        {
            var employees = employeeAgent.GetAllEmployees();

            if (!string.IsNullOrEmpty(email))
            {
                employees = employees.Where(e => e.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (employeeId.HasValue)
            {
                employees = employees.Where(e => e.Id == employeeId.Value).ToList();
            }

            return Ok(employees);
        }
    }
}