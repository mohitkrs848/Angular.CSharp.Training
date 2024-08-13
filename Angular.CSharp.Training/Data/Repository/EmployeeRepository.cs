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
                employees = employees.Where(e => e.EmpEmail.Equals(email, StringComparison.OrdinalIgnoreCase));
            }
            else if (employeeId.HasValue)
            {
                employees = employees.Where(e => e.Id == employeeId.Value);
            }

            return employees;
        }

        public void UpdateEmployee(Employee employee) => Update(employee);

        public IEnumerable<Employee> GetEmployeesByDepartment(string department)
        {
            return context.Employees
                           .Where(e => e.EmpDeptName == department)
                           .ToList();
        }

        public int GenerateEmployeeId()
        {
            // Assuming EmployeeId is stored as a string in the database
            var lastEmployee = context.Employees
                .OrderByDescending(e => e.Id)
                .FirstOrDefault();

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
        //public IEnumerable<Employee> GetEmployeesByDepartment(string departmentName)
        //{
        //    return context.Employees
        //                  .Where(e => e.Department.DeptName == departmentName)
        //                  .ToList();
        //}
    }
}