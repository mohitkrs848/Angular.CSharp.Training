using Angular.CSharp.Training.Agents;
using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Angular.CSharp.Training.Data.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DemoDbContext context) : base(context)
        {
        }

        public async Task CreateEmoployee(Employee employee)
        {
            await InsertAsync(employee);
        }

        public async Task DeleteEmployee(int id)
        {
            await DeleteAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await GetAllAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByFilter(string email = null, int? employeeId = null)
        {
            var employees = await GetAllEmployees();

            if (!string.IsNullOrEmpty(email))
            {
                employees = employees.Where(e => e.EmpEmail.Equals(email, StringComparison.OrdinalIgnoreCase));
            }
            else if (employeeId.HasValue)
            {
                employees = employees.Where(e => e.Id == employeeId.Value);
            }

            return employees;
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await UpdateAsync(employee);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartment(string department)
        {
            return await context.Employees
                           .Where(e => e.EmpDeptName == department)
                           .ToListAsync();
        }

        public async Task<int> GenerateEmployeeId()
        {
            var lastEmployee = await context.Employees
                .OrderByDescending(e => e.Id)
                .FirstOrDefaultAsync();

            int newId;

            if (lastEmployee == null)
            {
                newId = 100000;
            }
            else
            {
                newId = lastEmployee.Id + 1;
            }

            return newId;
        }

        public async Task<IEnumerable<Employee>> SearchEmployees(string email, int? id, string name)
        {
            var employees = await GetAllEmployees();

            if (!string.IsNullOrEmpty(email))
            {
                employees = employees.Where(e => e.EmpEmail.Contains(email)).ToList();
            }

            if (id.HasValue)
            {
                employees = employees.Where(e => e.Id == id.Value).ToList();
            }

            if (!string.IsNullOrEmpty(name))
            {
                employees = employees.Where(e => e.EmpFirstName.Contains(name) || e.EmpLastName.Contains(name)).ToList();
            }

            return employees;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees(string department, string designation, int? age, decimal? salaryMin, decimal? salaryMax, string location, string status, int? projectId)
        {
            var employees = await GetAllEmployees();

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

            return employees;
        }

        public async Task<object> GetDistinctValues()
        {
            var departments = (await GetAllEmployees()).Select(e => e.EmpDeptName).Distinct().ToList();
            var designations = (await GetAllEmployees()).Select(e => e.EmpDesignation).Distinct().ToList();
            var locations = (await GetAllEmployees()).Select(e => e.EmpLocation).Distinct().ToList();
            var projects = (await GetAllEmployees())
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

            return result;
        }

        public async Task<bool> CheckEmailExistence(string email, int? id)
        {
            var normalizedEmail = email.ToLowerInvariant(); // Normalize email
            var emailExists = (await GetAllEmployees()).Any(e => e.EmpEmail.ToLower() == normalizedEmail && (!id.HasValue || e.Id != id.Value));

            return emailExists;
        }
    }
}