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
        [HttpPost]
        public IActionResult arrangeDorm([FromBody]UserDorm userDorm)
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            var user = DBContext.DBstatic.Queryable<User>().Single(c => c.UserID == userID);
            if (user == null) return NoContent();
            else if (user.Access == 1)
            {
                int dormNum = DBContext.DBstatic.SqlQueryable<UserDorm>($"select * from userDorm where dormID = {userDorm.DormID}").ToList().Count;
                var dormMaxNum = DBContext.DBstatic.Queryable<Dorm>().Single(c => c.DormID == userDorm.DormID).MaxNum;
                if (dormNum >= dormMaxNum)
                {
                    return NoContent();
                }
                else
                {
                    DBContext.DBstatic.Insertable<UserDorm>(userDorm).ExecuteCommand();
                    return Ok(
                        new {
                            userDorm= userDorm
                        }
                        );
                }

            }
            else return NoContent();
        }
        [HttpPut]
        public IActionResult changeDorm([FromBody] UserDorm userDorm)
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            var user = DBContext.DBstatic.Queryable<User>().Single(c => c.UserID == userID);
            if (user == null) return NoContent();
            else if (user.Access == 1)
            {
                int dormNum = DBContext.DBstatic.SqlQueryable<UserDorm>($"select * from userDorm where dormID = {userDorm.DormID}").ToList().Count;
                var dormMaxNum = DBContext.DBstatic.Queryable<Dorm>().Single(c => c.DormID == userDorm.DormID).MaxNum;
                if (dormNum >= dormMaxNum)
                {
                    return NoContent();
                }
                else
                {
                    //此处需要修改
                    DBContext.DBstatic.Updateable<UserDorm>(userDorm).ExecuteCommand();
                    return Ok(
                        new
                        {
                            userDorm = userDorm 
                        }
                        );
                }

            }
            else return NoContent();
        }
    }
}
