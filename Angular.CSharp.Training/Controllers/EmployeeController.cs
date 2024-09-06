using Angular.CSharp.Training.Agents;
using Angular.CSharp.Training.Data;
using Angular.CSharp.Training.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Angular.CSharp.Training.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService employeeService;

        private readonly DemoDbContext demoDbContext;

        public EmployeeController(EmployeeService employeeService, DemoDbContext demoDbContext)
        {
            this.employeeService = employeeService;
            this.demoDbContext = demoDbContext;
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

        [HttpGet]
        [Route("download-template")]
        public IHttpActionResult DownloadTemplate()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employee Template");

                worksheet.Cells[1, 1].Value = "EmpFirstName";
                worksheet.Cells[1, 2].Value = "EmpLastName";
                worksheet.Cells[1, 3].Value = "EmpAge";
                worksheet.Cells[1, 4].Value = "EmpEmail";
                worksheet.Cells[1, 5].Value = "EmpDesignation";
                worksheet.Cells[1, 6].Value = "EmpManagerID";
                worksheet.Cells[1, 7].Value = "EmpDeptName";
                worksheet.Cells[1, 8].Value = "EmpStatus";
                worksheet.Cells[1, 9].Value = "EmpSalary";
                worksheet.Cells[1, 10].Value = "EmpLocation";
                worksheet.Cells[1, 11].Value = "ProjectId";

                using (var stream = new MemoryStream())
                {
                    package.SaveAs(stream);
                    byte[] byteArray = stream.ToArray();

                    var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(byteArray)
                    };
                    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "EmployeeTemplate.xlsx"
                    };
                    result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                    return ResponseMessage(result);
                }
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IHttpActionResult> UploadFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest("Unsupported media type.");
            }

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            List<Employee> employees = new List<Employee>();

            foreach (var file in provider.Contents)
            {
                if (file.Headers.ContentDisposition != null && file.Headers.ContentDisposition.FileName != null)
                {
                    var filename = file.Headers.ContentDisposition.FileName.Trim('"');
                    var buffer = await file.ReadAsByteArrayAsync();

                    if (filename.EndsWith(".csv"))
                    {
                        employees = ParseCsv(buffer);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        employees = ParseExcel(buffer);
                    }
                    else
                    {
                        return BadRequest("Unsupported file format.");
                    }

                    // Log employee data for inspection
                    foreach (var employee in employees)
                    {
                        //string errorMessage;
                        //if (!ValidateEmployeeData(employee, out errorMessage))
                        //{
                        //    return BadRequest($"Error in employee data: {errorMessage}");
                        //}

                        // Log the data
                        Console.WriteLine($"Employee Data: {employee.EmpFirstName}, {employee.EmpLastName}, {employee.EmpEmail}, {employee.EmpSalary}");

                        // Generate EmployeeId and save to the database
                        employee.Id = await employeeService.GenerateEmployeeId();
                        await employeeService.CreateEmployee(employee);
                    }
                }
                else
                {
                    return BadRequest("File content disposition is missing or invalid.");
                }
            }

            return Ok("File uploaded and processed successfully.");
        }


        private bool ValidateEmployeeData(Employee employee, out string errorMessage)
        {
            //// Example of validation logic
            //if (string.IsNullOrEmpty(employee.EmpFirstName))
            //{
            //    errorMessage = "First Name is required.";
            //    return false;
            //}

            //if (string.IsNullOrEmpty(employee.EmpEmail) || !IsValidEmail(employee.EmpEmail))
            //{
            //    errorMessage = "A valid email is required.";
            //    return false;
            //}

            //if (employee.EmpAge <= 0)
            //{
            //    errorMessage = "Age must be greater than 0.";
            //    return false;
            //}

            //if (employee.EmpSalary <= 0)
            //{
            //    errorMessage = "Salary must be greater than 0.";
            //    return false;
            //}

            //// Additional validation based on your business rules
            errorMessage = null;
            return true;
        }

        private List<Employee> ParseCsv(byte[] fileContent)
        {
            var employees = new List<Employee>();

            using (var memoryStream = new MemoryStream(fileContent))
            using (var reader = new StreamReader(memoryStream))
            {
                string line;
                bool firstLine = true; // Skip header

                while ((line = reader.ReadLine()) != null)
                {
                    if (firstLine)
                    {
                        firstLine = false;
                        continue; // Skip the header line
                    }

                    var values = line.Split(',');

                    var employee = new Employee
                    {
                        EmpFirstName = values[0],
                        EmpLastName = values[1],
                        EmpAge = int.Parse(values[2]),
                        EmpEmail = values[3],
                        EmpDesignation = values[4],
                        EmpManagerID = int.Parse(values[5]),
                        EmpDeptName = values[6],
                        EmpStatus = values[7],
                        EmpSalary = decimal.Parse(values[8]),
                        EmpLocation = values[9],
                        ProjectId = int.Parse(values[10])
                    };

                    employees.Add(employee);
                }
            }
            return employees;
        }

        private List<Employee> ParseExcel(byte[] fileContent)
        {
            var employees = new List<Employee>();

            using (var memoryStream = new MemoryStream(fileContent))
            using (var package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assume the data is in the first worksheet
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) // Start at row 2 to skip the header
                {
                    var firstName = worksheet.Cells[row, 1].Text;
                    var lastName = worksheet.Cells[row, 2].Text;
                    var ageText = worksheet.Cells[row, 3].Text;
                    var email = worksheet.Cells[row, 4].Text;
                    var designation = worksheet.Cells[row, 5].Text;
                    var managerIdText = worksheet.Cells[row, 6].Text;
                    var deptName = worksheet.Cells[row, 7].Text;
                    var status = worksheet.Cells[row, 8].Text;
                    var salaryText = worksheet.Cells[row, 9].Text;
                    var location = worksheet.Cells[row, 10].Text;
                    var projectIdText = worksheet.Cells[row, 11].Text;

                    Console.WriteLine($"Row {row}: {firstName}, {lastName}, {ageText}, {email}, {designation}, {managerIdText}, {deptName}, {status}, {salaryText}, {location}, {projectIdText}");

                    try
                    {
                        var employee = new Employee
                        {
                            EmpFirstName = firstName,
                            EmpLastName = lastName,
                            EmpAge = int.Parse(ageText),
                            EmpEmail = email,
                            EmpDesignation = designation,
                            EmpManagerID = int.Parse(managerIdText),
                            EmpDeptName = deptName,
                            EmpStatus = status,
                            EmpSalary = decimal.Parse(salaryText),
                            EmpLocation = location,
                            ProjectId = int.Parse(projectIdText)
                        };

                        employees.Add(employee);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Format exception at row {row}: {ex.Message}");
                        throw; // Re-throw the exception for further handling
                    }
                }
            }
            return employees;
        }
    }
}