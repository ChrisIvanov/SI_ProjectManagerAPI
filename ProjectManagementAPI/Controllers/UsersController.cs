using Microsoft.AspNetCore.Mvc;
using Common.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            ProjectManagementDbContext context = new ProjectManagementDbContext();
            List<User> res = context.Users.ToList();
            
            context.Dispose();

            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ProjectManagementDbContext context = new ProjectManagementDbContext();
            User user = context.Users
                                .Where(i => i.Id == id)
                                .FirstOrDefault();

            context.Dispose();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User item)
        {
            ProjectManagementDbContext context = new ProjectManagementDbContext();
            context.Users.Add(item);
            context.SaveChanges();

            context.Dispose();

            return Ok(item);
        }

        [HttpPut]
        public IActionResult Put([FromBody] User item)
        {
            ProjectManagementDbContext context = new ProjectManagementDbContext();
            User user = context.Users
                                .Where(i => i.Id == item.Id)
                                .FirstOrDefault();

            user.Username = item.Username;
            user.Password = item.Password;
            user.FirstName = item.FirstName;
            user.LastName = item.LastName;

            context.Users.Update(user);
            context.SaveChanges();

            context.Dispose();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ProjectManagementDbContext context = new ProjectManagementDbContext();
            User item = context.Users
                                .Where(i => i.Id == id)
                                .FirstOrDefault();
            if (item != null)
            {
                context.Users.Remove(item);
                context.SaveChanges();

                context.Dispose();

                return Ok(item);
            }

            context.Dispose();

            return NotFound();
        }
    }
}
