using Common.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Models.Projects;

namespace ProjectManagementAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class ProjectsController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      ProjectManagementDbContext context = new ProjectManagementDbContext();
      List<Project> res = context.Projects.ToList();

      context.Dispose();

      return Ok(res);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      ProjectManagementDbContext context = new ProjectManagementDbContext();
      Project res = context.Projects
                                    .Where(p => p.Id == id)
                                    .FirstOrDefault();

      context.Dispose();

      return Ok(res);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateModel model)
    {
      int loggedUserId = Convert.ToInt32(this.User.FindFirst("loggedUserId").Value);

      ProjectManagementDbContext context = new ProjectManagementDbContext();
      User loggedUser = context.Users
                                  .Where(u => u.Id == loggedUserId)
                                  .FirstOrDefault();

      if (loggedUser == null)
        return NotFound();

      Project item = new Project();
      item.Title = model.Title;
      item.Description = model.Description;
      item.OwnerId = loggedUser.Id;

      context.Projects.Add(item);
      context.SaveChanges();

      context.Dispose();

      return Ok(item);
    }

    [HttpPut]
    public IActionResult Put([FromBody]UpdateModel model)
    {
      ProjectManagementDbContext context = new ProjectManagementDbContext();
      Project item = context.Projects
                          .Where(i => i.Id == model.Id)
                          .FirstOrDefault();

      item.Title = model.Title;
      item.Description = model.Description;

      context.Projects.Update(item);
      context.SaveChanges();

      context.Dispose();

      return Ok(item);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      ProjectManagementDbContext context = new ProjectManagementDbContext();
      Project item = context.Projects
                          .Where(i => i.Id == id)
                          .FirstOrDefault();
      if (item != null)
      {
        context.Projects.Remove(item);
        context.SaveChanges();
      }
      context.Dispose();

      return Ok(item);
    }
  }
}
