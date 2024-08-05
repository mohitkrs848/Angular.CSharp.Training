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

        public void UpdateEmployee(Employee employee) => Update(employee);
    }
}