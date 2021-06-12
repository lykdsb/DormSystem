using System;
using Microsoft.AspNetCore.Mvc;
using backend.Mappers;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
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
            User user;
            UserDorm userDorm;
            List<User> users;
            try
            {
                user = UserMapper.GetUserByID(userID);
                userDorm = UserDormMapper.GetUserDormByUserID(userID);
                users = UserMapper.getUsers();
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    success =0,
                    msg=e.Message
                }
                );
            }
            if (user == null) return Ok(new
            {
                success = 1,
                msg = "No this user"
            });
            else if (user.Access == 0)
                return Ok(new
                {
                    success =1,
                    userDorm =userDorm
                }
                );
            else
            {
                foreach (User u in users)
                {
                    u.Password = "";
                }
                return Ok(
                 new
                 {
                     success=1,
                     userList = users
                 }
                 );
            }
            
        }
    }
}
