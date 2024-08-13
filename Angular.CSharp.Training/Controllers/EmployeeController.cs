using Angular.CSharp.Training.Agents;
using Angular.CSharp.Training.Data;
using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Angular.CSharp.Training.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private EmployeeAgent employeeAgent;

        protected readonly DemoDbContext context;
        //protected readonly DbSet<T> dbSet;

        public EmployeeController(EmployeeAgent employeeAgent)
        {
            context = new DemoDbContext();
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

            employee.Id = employeeAgent.GenerateEmployeeId();

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

        //[HttpGet]
        //[Route("")]
        //public IEnumerable<Employee> GetAllEmployees()
        //{
        //    return employeeAgent.GetAllEmployees();
        //}

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
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
        public IHttpActionResult SearchEmployees(string email = null, int? id = null, string name = null)
        {
            var employees = employeeAgent.SearchEmployees(email, id, name);
            return Ok(employees);
        }
        
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetEmployees(string department = null, string designation = null)
        {
            try
            {
                var employees = employeeAgent.GetAllEmployees();

                if (!string.IsNullOrEmpty(department))
                {
                    employees = employees.Where(e => e.EmpDeptName == department).ToList();
                }

                if (!string.IsNullOrEmpty(designation))
                {
                    employees = employees.Where(e => e.EmpDesignation == designation).ToList();
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("checkemail")]
        public IHttpActionResult CheckEmail(string email, int? id = null)
        {
            try
            {
                var normalizedEmail = email.ToLowerInvariant(); // Normalize email
                var emailExists = context.Employees.Any(e => e.EmpEmail.ToLower() == normalizedEmail && (!id.HasValue || e.Id != id.Value));

                return Ok(new { isUnique = !emailExists });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}