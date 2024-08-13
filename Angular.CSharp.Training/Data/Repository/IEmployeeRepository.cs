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
        void CreateEmoployee(Employee employee);
        void DeleteEmployee(int id);
        void UpdateEmployee(Employee employee);
        Employee GetEmployeeById(int id);
        IEnumerable<Employee> GetAllEmployees();
        IEnumerable<Employee> GetEmployeesByFilter(string email, int? employeeId);
        IEnumerable<Employee> GetEmployeesByDepartment(string department);
        int GenerateEmployeeId();
        //IEnumerable<Employee> GetEmployeesByDepartment(string departmentName);
    }
}
