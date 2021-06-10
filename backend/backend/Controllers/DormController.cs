using System;
using Microsoft.AspNetCore.Mvc;
using backend.Configs;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;

namespace backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class DormController:ControllerBase
    {
        [HttpGet]
        public IActionResult getDormInfo()
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            var user = DBContext.DBstatic.Queryable<User>().Single(c => c.UserID == userID);
            if (user == null) return NoContent();
            else if (user.Access == 1)
            {
                var userDorms = DBContext.DBstatic.Queryable<UserDorm>().ToList();
                return Ok(
                 new
                 {
                     dormList = userDorms
                 }
                 );
            }
            else
            {
                var dormID = DBContext.DBstatic.Queryable<UserDorm>().Single(c => c.UserID == userID).DormID;
                var roomates = DBContext.DBstatic.Queryable<UserDorm>().Where(c => c.DormID == dormID).ToList();
                List<int> roomateID=new List<int>();
                foreach (UserDorm ud in roomates)
                {
                    roomateID.Add(ud.UserID);
                }
                return Ok(
                    new
                    {
                        dormID = dormID,
                        roomateID = roomateID
                    }
                    );
            }
        }
    }
}
