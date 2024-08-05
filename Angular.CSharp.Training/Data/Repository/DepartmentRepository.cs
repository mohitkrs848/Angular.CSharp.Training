using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angular.CSharp.Training.Data.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DemoDbContext context) : base(context)
        {
        }

        public void CreateDepartment(Department department) => Insert(department);

        public void DeleteDepartment(int id) => Delete(id);
        public void UpdateDepartment(Department department) => Update(department);

        public IEnumerable<Department> GetAllDepartments() => GetAll();
        public Department GetDepartmentById(int id) => GetById(id);
    }
}