using System;
using Microsoft.AspNetCore.Mvc;
using backend.Configs;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Security.Claims;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController:ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            var user = DBContext.DBstatic.Queryable<User>().Single(c => c.UserID == userID);
            if (user == null) return NoContent();
            else if (user.Access == 0)
            {
                var userDorm = DBContext.DBstatic.Queryable<UserDorm>().Single(c => c.UserId == userID);
                if (userDorm == null) return NoContent();
                return Ok(
                    new
                    {
                        userID = user.UserID,
                        userName = user.UserName,
                        dormID = userDorm.DormID
                    }
                    );
            }
            else
            {
                var users = DBContext.DBstatic.Queryable<User>().ToList();
                foreach (User u in users)
                {
                    u.Password = "";
                }
                   return Ok(
                    new {
                        userList = users
                    }
                    );
            }
            
        }
    }
}
