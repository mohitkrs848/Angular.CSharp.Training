using Angular.CSharp.Training.Data;
using Angular.CSharp.Training.Data.Repository;
using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;

namespace Angular.CSharp.Training.Agents
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task CreateEmployee(Employee employee) => await _employeeRepository.CreateEmoployee(employee);
        public async Task DeleteEmployee(int id) => await _employeeRepository.DeleteEmployee(id);
        public async Task UpdateEmployee(Employee employee) => await _employeeRepository.UpdateEmployee(employee);
        public async Task<Employee> GetEmployeeById(int id) => await _employeeRepository.GetEmployeeById(id);
        public async Task<IEnumerable<Employee>> GetAllEmployees() => await _employeeRepository.GetAllEmployees();
        public async Task<IEnumerable<Employee>> GetEmployeesByFilter(string email, int employeeId) => await _employeeRepository.GetEmployeesByFilter(email, employeeId);

        public async Task<IEnumerable<Employee>> SearchEmployees(string email, int? id, string name) => await _employeeRepository.SearchEmployees(email, id, name);


        public async Task<IEnumerable<Employee>> GetEmployeesByDepartment(string department) => await _employeeRepository.GetEmployeesByDepartment(department);

        public async Task<int> GenerateEmployeeId() => await _employeeRepository.GenerateEmployeeId();

        public async Task<IEnumerable<Employee>> GetAllEmployees(string department, string designation, int? age, decimal? salaryMin, decimal? salaryMax, string location, string status, int? projectId)
            => await _employeeRepository.GetAllEmployees(department, designation, age, salaryMin, salaryMax, location, status, projectId);

        internal async Task<object> GetDistinctValues() => await _employeeRepository.GetDistinctValues();

        internal async Task<bool> CheckEmailExistence(string email, int? id) => await _employeeRepository.CheckEmailExistence(email, id);
    }

}