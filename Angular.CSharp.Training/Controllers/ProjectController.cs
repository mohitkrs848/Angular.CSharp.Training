using Angular.CSharp.Training.Data;
using Angular.CSharp.Training.Models;
using Angular.CSharp.Training.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

[RoutePrefix("api/project")]
public class ProjectController : ApiController
{
    private readonly IProjectService projectService;

    public ProjectController(ProjectService projectService)
    {
        this.projectService = projectService;
    }

    // GET: api/project
    [HttpGet]
    [Route("")]
    public async Task<IHttpActionResult> GetAllProjects()
    {
        try
        {
            var projects = await projectService.GetAllProjects();
            return Ok(projects);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return InternalServerError(ex);
        }
    }

    // GET: api/project/{id}
    [HttpGet]
    [Route("{id}")]
    public async Task<IHttpActionResult> GetProjectById(int id)
    {
        try
        {
            var project = await projectService.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    // POST: api/project
    [HttpPost]
    [Route("")]
    public async Task<IHttpActionResult> CreateProject([FromBody] Project project)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await projectService.CreateProject(project);
            return Ok(project);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    // PUT: api/project/{id}
    [HttpPut]
    [Route("{id}")]
    public async Task<IHttpActionResult> UpdateProject(int id, [FromBody] Project project)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await projectService.UpdateProject(id, project);

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    // DELETE: api/project/{id}
    [HttpDelete]
    [Route("{id}")]
    public async Task<IHttpActionResult> DeleteProject(int id)
    {
        try
        {
            await projectService.DeleteProject(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
}