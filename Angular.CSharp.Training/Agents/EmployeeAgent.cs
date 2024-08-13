using Angular.CSharp.Training.Data;
using Angular.CSharp.Training.Data.Repository;
using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angular.CSharp.Training.Agents
{
    public class EmployeeAgent
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeAgent(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        public void CreateEmployee(Employee employee) => _employeeRepository.CreateEmoployee(employee);
        public void DeleteEmployee(int id) => _employeeRepository.DeleteEmployee(id);
        public void UpdateEmployee(Employee employee) => _employeeRepository.UpdateEmployee(employee);
        public Employee GetEmployeeById(int id) => _employeeRepository.GetEmployeeById(id);
        public IEnumerable<Employee> GetAllEmployees() => _employeeRepository.GetAllEmployees();
        public IEnumerable<Employee> GetEmployeesByFilter(string email, int employeeId) => _employeeRepository.GetEmployeesByFilter(email, employeeId);

        public IEnumerable<Employee> SearchEmployees(string email, int? id, string name)
        {
            var employees = _employeeRepository.GetAllEmployees();

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

        public IEnumerable<Employee> GetEmployeesByDepartment(string department) => _employeeRepository.GetEmployeesByDepartment(department);

        public int GenerateEmployeeId() => _employeeRepository.GenerateEmployeeId();

        //public IEnumerable<Employee> GetEmployeesByDepartment(string departmentName)
        //{
        //    // Define allowed department names
        //    var allowedDepartments = new List<string> { "HR", "Sales", "Engineering" };

        //    if (!allowedDepartments.Contains(departmentName))
        //    {
        //        throw new ArgumentException("Invalid department name");
        //    }

        //    return _employeeRepository.GetEmployeesByDepartment(departmentName);
        //}
    }

}