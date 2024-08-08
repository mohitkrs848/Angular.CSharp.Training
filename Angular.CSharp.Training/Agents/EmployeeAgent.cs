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
    }
}