using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Timelogger.Abstract;
using Timelogger.Api.Abstract;
using Timelogger.Entities;
using Timelogger.Filters;

namespace Timelogger.Api.Controllers
{
	[Route("api/[controller]")]
    [ServiceFilter(typeof(ValidateModelState))]
    [ServiceFilter(typeof(ValidationUserType))]
    public class ProjectsController : Controller, IProjects
    {
        private readonly IProjectsRepositories projectsRepositories;

		public ProjectsController(IProjectsRepositories projectsRepositories)
		{
            this.projectsRepositories = projectsRepositories;
		}

        [HttpGet]
        public IActionResult Get(int id = 0, string name = null)
        {
            Project project = null;
            if (id != 0)
            {
                project = projectsRepositories.GetProject(id);
            }
            if (!string.IsNullOrEmpty(name))
            {
                project = projectsRepositories.GetProjectByName(name);
            }
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Project project)
        {
            if(project == null)
            {
                return BadRequest();
            }
            if(projectsRepositories.GetProjectByName(project.Name) != null)
            {
                return Conflict("Project with this name already exists!");
            }
            if(project.Id != 0)
            {
                project.Id = 0;
            }
            projectsRepositories.AddProject(project);
            return Ok();
        }
        [HttpPut]
        public IActionResult Put([FromBody] Project project)
        {
            if (project == null)
            {
                return BadRequest();
            }
            if (project.Id != 0)
            {
                project.Id = 0;
            }
            var oldProject = project;
            projectsRepositories.UpdateProject(project);
            return Ok(new { OldProject = oldProject, NewProject = project });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {

            projectsRepositories.DeleteProject(id);
            return Ok();
        }
        [HttpGet]
        [Route("GetProjectsOrdered")]
        public IActionResult GetByDeadline(bool orderDesc = false)
        {
            return Ok(projectsRepositories.GetAllProjects(orderDesc));
        }

    }
}
