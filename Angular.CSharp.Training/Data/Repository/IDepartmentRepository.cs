using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular.CSharp.Training.Data.Repository
{
    internal interface IDepartmentRepository
    {
        void CreateDepartment(Department department);
        void DeleteDepartment(int id);
        void UpdateDepartment(Department department);
        Department GetDepartmentById(int id);
        IEnumerable<Department> GetAllDepartments();

    }
}
