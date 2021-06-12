using System;
using Microsoft.AspNetCore.Mvc;
using backend.Mappers;
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
            int userID;
            try
            {
                userID = UserMapper.addUser(userInput);
            }
            catch (Exception e)
            {
                return Ok(
                    new
                    {
                        success = 0,
                        msg=e.Message
                    }
                    );
            }
            return Ok(new {
                success =1,
                userID = userID
            });
        }
    }
}
