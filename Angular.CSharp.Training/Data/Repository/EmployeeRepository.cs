using Angular.CSharp.Training.Agents;
using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angular.CSharp.Training.Data.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DemoDbContext context) : base(context)
        {
        }

        public void CreateEmoployee(Employee employee) => Insert(employee);

        public void DeleteEmployee(int id) => Delete(id);

        public IEnumerable<Employee> GetAllEmployees() => GetAll();
        public Employee GetEmployeeById(int id) => GetById(id);

        public IEnumerable<Employee> GetEmployeesByFilter(string email = null, int? employeeId = null)
        {
            var employees = GetAllEmployees();

            if (!string.IsNullOrEmpty(email))
            {
                // Filter by email if provided
                employees = employees.Where(e => e.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            }
            else if (employeeId.HasValue)
            {
                // Filter by employee ID if provided
                employees = employees.Where(e => e.Id == employeeId.Value);
            }

            return employees;
        }

        public void UpdateEmployee(Employee employee) => Update(employee);
    }
}