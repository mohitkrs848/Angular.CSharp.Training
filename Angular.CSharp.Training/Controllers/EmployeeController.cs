using Angular.CSharp.Training.Agents;
using Angular.CSharp.Training.Data;
using Angular.CSharp.Training.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Angular.CSharp.Training.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                employee.Id = await employeeService.GenerateEmployeeId();

                await employeeService.CreateEmployee(employee);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await employeeService.GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != employee.Id)
                {
                    return BadRequest();
                }
                await employeeService.UpdateEmployee(employee);
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await employeeService.GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound();
                }
                await employeeService.DeleteEmployee(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("search")]
        public async Task<IHttpActionResult> SearchEmployees(string query)
        {
            try
            {
                var employees = await employeeService.SearchEmployees(query);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetEmployees(string department = null,
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
                var employees = await employeeService.GetAllEmployees(department, designation, age, salaryMin, salaryMax, location, status, projectId);

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("distinct-values")]
        public async Task<IHttpActionResult> GetDistinctValues()
        {
            try
            {
                var result = await employeeService.GetDistinctValues();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("checkemail")]
        public async Task<IHttpActionResult> CheckEmail(string email, int? id = null)
        {
            try
            {
                var emailExists = await employeeService.CheckEmailExistence(email, id);

                return Ok(new { isUnique = !emailExists });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}