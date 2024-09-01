using Angular.CSharp.Training.Data.Repository;
using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;

namespace Angular.CSharp.Training.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task CreateProject(Project project)
        {
            await _projectRepository.CreateProject(project);
        }

        public async Task DeleteProject(int id)
        {
            await _projectRepository.DeleteProject(id);
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _projectRepository.GetAllProjects();
        }

        public async Task<Project> GetProjectById(int id)
        {
            return await _projectRepository.GetProject(id);
        }

        public async Task UpdateProject(int id, Project project)
        {
            await _projectRepository.UpdateProject(id, project);
        }
    }
}