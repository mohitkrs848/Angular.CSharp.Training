using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular.CSharp.Training.Data.Repository
{
    public interface IEmployeeRepository
    {
        Task CreateEmoployee(Employee employee);
        Task DeleteEmployee(int id);
        Task UpdateEmployee(Employee employee);
        Task<Employee> GetEmployeeById(int id);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<IEnumerable<Employee>> GetEmployeesByFilter(string email, int? employeeId);
        Task<IEnumerable<Employee>> GetEmployeesByDepartment(string department);
        Task<int> GenerateEmployeeId();
        Task<IEnumerable<Employee>> SearchEmployees(string query);
        Task<IEnumerable<Employee>> GetAllEmployees(string department, string designation, int? age, decimal? salaryMin, decimal? salaryMax, string location, string status, int? projectId);
        Task<object> GetDistinctValues();
        Task<bool> CheckEmailExistence(string email, int? id);
    }
}
