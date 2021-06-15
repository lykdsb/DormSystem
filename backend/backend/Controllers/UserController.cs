using System;
using Microsoft.AspNetCore.Mvc;
using backend.Mappers;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController:ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            User user;
            UserDorm userDorm;
            List<User> users;
            try
            {
                user = await UserMapper.GetUserByID(userID);
                if (user.Access == 0)
                {
                    userDorm = await UserDormMapper.GetUserDormByUserID(userID);
                    return Ok(new
                    {
                        success = 1,
                        userDorm = userDorm
                    }
                    );
                }
                else
                {
                    users = await UserMapper.GetUsers();
                    foreach (User u in users)
                    {
                        u.Password = "";
                    }
                    return Ok(
                     new
                     {
                         success = 1,
                         userList = users
                     }
                     );
                }
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    success = 0,
                    msg = e.Message
                }
                );
            }

        }
    }
}
