using Angular.CSharp.Training.Agents;
using Angular.CSharp.Training.Data;
using Angular.CSharp.Training.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace Angular.CSharp.Training.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private EmployeeAgent employeeAgent;

        protected readonly DemoDbContext context;

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
        public IHttpActionResult GetEmployees(string department = null,
            string designation = null,
            int? age = null,
            decimal? salaryMin = null,
            decimal? salaryMax = null,
            string location = null,
            string status = null,
            int? projectId = null)
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

                if (age.HasValue)
                {
                    employees = employees.Where(e => e.EmpAge == age.Value).ToList();
                }

                if (salaryMin.HasValue)
                {
                    employees = employees.Where(e => e.EmpSalary >= salaryMin.Value).ToList();
                }

                if (salaryMax.HasValue)
                {
                    employees = employees.Where(e => e.EmpSalary <= salaryMax.Value).ToList();
                }

                if (!string.IsNullOrEmpty(location))
                {
                    employees = employees.Where(e => e.EmpLocation == location).ToList();
                }

                if (!string.IsNullOrEmpty(status))
                {
                    employees = employees.Where(e => e.EmpStatus == status).ToList();
                }

                if (projectId.HasValue)
                {
                    employees = employees.Where(e => e.ProjectId == projectId.Value).ToList();
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("distinct-values")]
        public IHttpActionResult GetDistinctValues()
        {
            try
            {
                var departments = employeeAgent.GetAllEmployees().Select(e => e.EmpDeptName).Distinct().ToList();
                var designations = employeeAgent.GetAllEmployees().Select(e => e.EmpDesignation).Distinct().ToList();
                var locations = employeeAgent.GetAllEmployees().Select(e => e.EmpLocation).Distinct().ToList();
                var projects = employeeAgent.GetAllEmployees()
                                .Where(e => e.Project != null)
                                .Select(e => new { e.Project.Id, e.Project.ProjectName })
                                .Distinct()
                                .ToList();

                var result = new
                {
                    Departments = departments,
                    Designations = designations,
                    Locations = locations,
                    Projects = projects
                };

                return Ok(result);
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