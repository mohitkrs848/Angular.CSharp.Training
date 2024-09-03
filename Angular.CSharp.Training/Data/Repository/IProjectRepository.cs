using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular.CSharp.Training.Data.Repository
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProject(int id);
        Task CreateProject(Project project);
        Task UpdateProject(int id, Project project);
        Task DeleteProject(int id);
    }
}
