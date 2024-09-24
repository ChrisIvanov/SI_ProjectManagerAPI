using Common.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(string username, string password)
        {
            ProjectManagementDbContext context = new ProjectManagementDbContext();
            User loggedUser = context.Users
                                  .Where(u =>
                                      u.Username == username &&
                                      u.Password == password
                                  )
                                  .FirstOrDefault();

            if (loggedUser == null)
                return NotFound();

            var claims = new Claim[]
            {
                new Claim("loggedUserId", loggedUser.Id.ToString())
            };

            var symmetricKey =
              new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes("!Password123!Password123!Password123")
              );
            var signingCredentials =
                new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
              "fmi",
              "project-management-system",
              claims,
              null,
              DateTime.Now.AddMinutes(10),
              signingCredentials
            );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string jwtString = handler.WriteToken(jwt).ToString();

            return Ok(new
            {
                token = jwtString
            });
        }
    }
}
