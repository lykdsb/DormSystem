using System;
using Microsoft.AspNetCore.Mvc;
using backend.Mappers;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController:ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] User userInput)
        {
            int userID;
            try
            {
                userID = await UserMapper.AddUser(userInput);
                return Ok(new
                {
                    success = 1,
                    userID = userID
                });
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

        }
    }
}
