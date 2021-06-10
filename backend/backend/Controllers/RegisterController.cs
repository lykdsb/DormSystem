using System;
using Microsoft.AspNetCore.Mvc;
using backend.Configs;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController:ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Submit([FromBody] User userInput)
        {
            try
            {              
                DBContext.DBstatic.Insertable<User>(userInput).ExecuteCommand();
                userInput.UserID = DBContext.DBstatic.Queryable<User>().Max<int>("userID");
            }
            catch (Exception e)
            {
                return Conflict(new { Error = e.Message });
            }
            return Ok(new {
                userID = userInput.UserID
            });
        }
    }
}
