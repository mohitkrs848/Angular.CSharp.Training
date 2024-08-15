using Angular.CSharp.Training.Data;
using Angular.CSharp.Training.Models;
using System.Linq;
using System.Web.Http;

[RoutePrefix("api/project")]
public class ProjectController : ApiController
{
    private readonly DemoDbContext context;

    public ProjectController()
    {
        context = new DemoDbContext();
    }

    // GET: api/project
    [HttpGet]
    [Route("")]
    public IHttpActionResult GetAllProjects()
    {
        var projects = context.Projects.ToList();
        return Ok(projects);
    }

    // GET: api/project/{id}
    [HttpGet]
    [Route("{id}")]
    public IHttpActionResult GetProject(int id)
    {
        var project = context.Projects.Find(id);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }

    // POST: api/project
    [HttpPost]
    [Route("")]
    public IHttpActionResult CreateProject([FromBody] Project project)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Projects.Add(project);
            context.SaveChanges();
            return Ok(project);
        }
        catch (System.Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    // PUT: api/project/{id}
    [HttpPut]
    [Route("{id}")]
    public IHttpActionResult UpdateProject(int id, [FromBody] Project project)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingProject = context.Projects.Find(id);
        if (existingProject == null)
        {
            return NotFound();
        }

        existingProject.ProjectName = project.ProjectName;
        existingProject.ProjectStatus = project.ProjectStatus;
        existingProject.ProjectLocation = project.ProjectLocation;

        context.SaveChanges();
        return StatusCode(System.Net.HttpStatusCode.NoContent);
    }

    // DELETE: api/project/{id}
    [HttpDelete]
    [Route("{id}")]
    public IHttpActionResult DeleteProject(int id)
    {
        var project = context.Projects.Find(id);
        if (project == null)
        {
            return NotFound();
        }

        context.Projects.Remove(project);
        context.SaveChanges();
        return Ok(project);
    }
}