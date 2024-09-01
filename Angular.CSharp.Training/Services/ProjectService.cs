using Angular.CSharp.Training.Data.Repository;
using Angular.CSharp.Training.Models;
using Serilog;
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
        private readonly ILogger logger;

        public ProjectService(IProjectRepository projectRepository, ILogger logger)
        {
            _projectRepository = projectRepository;
            this.logger = logger;
        }

        public async Task CreateProject(Project project)
        {
            try
            {
                await _projectRepository.CreateProject(project);
                logger.Information("Project created successfully.");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while creating project.");
                throw;
            }
        }

        public async Task DeleteProject(int id)
        {
            try
            {
                await _projectRepository.DeleteProject(id);
                logger.Information("Project deleted successfully.");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while deleting project.");
                throw;
            }
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            try
            {
                var projects = await _projectRepository.GetAllProjects();
                logger.Information("Retrieved all projects successfully.");
                return projects;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while retrieving all projects.");
                throw;
            }
        }

        public async Task<Project> GetProjectById(int id)
        {
            try
            {
                var project = await _projectRepository.GetProject(id);
                logger.Information("Retrieved project by ID successfully.");
                return project;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while retrieving project by ID.");
                throw;
            }
        }

        public async Task UpdateProject(int id, Project project)
        {
            try
            {
                await _projectRepository.UpdateProject(id, project);
                logger.Information("Project updated successfully.");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while updating project.");
                throw;
            }
        }
    }
}