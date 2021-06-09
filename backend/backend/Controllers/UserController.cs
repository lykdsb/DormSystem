using System;
using Microsoft.AspNetCore.Mvc;
using backend.Configs;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController:ControllerBase
    {
        public IActionResult GetUsers()
        {
            var users=DBContext.DBstatic.Queryable<User>().ToList();
            return Ok(users);
        }
    }
}
