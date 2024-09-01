using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Angular.CSharp.Training.Data.Repository
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DemoDbContext demoDbContext) : base(demoDbContext)
        {
        }

        public async Task CreateProject(Project project)
        {
            await InsertAsync(project);
        }

        public async Task DeleteProject(int id)
        {
            await DeleteAsync(id);
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await GetAllAsync();
        }

        public async Task<Project> GetProject(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task UpdateProject(int id, Project project)
        {
            await UpdateAsync(project);
        }
    }
}