using Angular.CSharp.Training.Agents;
using Angular.CSharp.Training.Data;
using Angular.CSharp.Training.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Employee Template");

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

                var stream = new MemoryStream();
                package.SaveAs(stream);
                byte[] byteArray = stream.ToArray();

                // Return the file to the user
                var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(byteArray)
                };
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "EmployeeTemplate.xlsx"
                };
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                return ResponseMessage(result);
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

            foreach (var file in provider.Contents)
            {
                if (file.Headers.ContentDisposition != null && file.Headers.ContentDisposition.FileName != null)
                {
                    var filename = file.Headers.ContentDisposition.FileName.Trim('"');
                    var buffer = await file.ReadAsByteArrayAsync();

                    // Check the file extension (CSV or Excel)
                    if (filename.EndsWith(".csv"))
                    {
                        // Process CSV file
                        var employees = ParseCsv(buffer);
                        foreach (var employee in employees)
                        {
                            employee.Id = await employeeService.GenerateEmployeeId();
                            await employeeService.CreateEmployee(employee);
                        }
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        // Process Excel file
                        var employees = ParseExcel(buffer);
                        foreach (var employee in employees)
                        {
                            employee.Id = await employeeService.GenerateEmployeeId();
                            await employeeService.CreateEmployee(employee);
                        }
                    }
                    else
                    {
                        return BadRequest("Unsupported file format.");
                    }
                }
            }
            return Ok("File uploaded and processed successfully.");
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
                    var employee = new Employee
                    {
                        EmpFirstName = worksheet.Cells[row, 1].Text,
                        EmpLastName = worksheet.Cells[row, 2].Text,
                        EmpAge = int.Parse(worksheet.Cells[row, 3].Text),
                        EmpEmail = worksheet.Cells[row, 4].Text,
                        EmpDesignation = worksheet.Cells[row, 5].Text,
                        EmpManagerID = int.Parse(worksheet.Cells[row, 6].Text),
                        EmpDeptName = worksheet.Cells[row, 7].Text,
                        EmpStatus = worksheet.Cells[row, 8].Text,
                        EmpSalary = decimal.Parse(worksheet.Cells[row, 9].Text),
                        EmpLocation = worksheet.Cells[row, 10].Text,
                        ProjectId = int.Parse(worksheet.Cells[row, 11].Text)
                    };

                    employees.Add(employee);
                }
            }
            return employees;
        }
    }
}