using System;
using Microsoft.AspNetCore.Mvc;
using backend.Configs;
using backend.Models;
namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController:ControllerBase
    {
        public IActionResult GetUsers()
        {
            var users=DBContext.DBstatic.Queryable<User>().ToList();
            return Ok(users);
        }
    }
}
