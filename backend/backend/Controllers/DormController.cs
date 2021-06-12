using System;
using Microsoft.AspNetCore.Mvc;
using backend.Configs;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using backend.Mappers;
namespace backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class DormController:ControllerBase
    {
        [HttpGet]
        public IActionResult GetDormInfo()
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            User user;
            List<UserDorm> userDorms;
            List<UserDorm> roomates;
            int dormID;
            try
            {
                user = UserMapper.GetUserByID(userID);
                userDorms = UserDormMapper.GetUserDorms();
                dormID = UserDormMapper.GetDormID(userID);
                roomates = UserDormMapper.GetRoomates(dormID);
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    success = 0,
                    msg = e.Message
                });
            }
            if (user == null)
                return Ok(new
                {
                    success =0,
                    msg= "No this user"
                });
            else if (user.Access == 1)
            {
                return Ok(new
                 {
                     success = 1,
                     dormList = userDorms
                 }
                 );
            }
            else
            {
                List<int> roomateID = new List<int>();
                foreach (UserDorm ud in roomates)
                {
                    roomateID.Add(ud.UserID);
                }
                return Ok(
                    new
                    {
                        success =1 ,
                        dormID = dormID,
                        roomateID = roomateID
                    }
                    );
            }
        }
        [HttpPost]
        public IActionResult ArrangeDorm([FromBody]UserDorm userDorm)
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            var user = UserMapper.GetUserByID(userID);
            int dormNum;
            int dormMaxNum;
            try
            {
                dormNum = UserDormMapper.GetDormNum(userDorm.DormID);
                dormMaxNum = DormMapper.GetMaxNum(userDorm.DormID);
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    success = 0,
                    msg = e.Message
                });
            }
            if (user.Access == 1)
            {
                if (dormNum >= dormMaxNum)
                {
                    return Ok(new
                    {
                        success =0,
                        msg="No more position"
                    });
                }
                else
                {
                    try
                    {
                        UserDormMapper.ArrangeDorm(userDorm);
                    }
                    catch (Exception e)
                    {
                        return Ok(new
                        {
                            success = 0,
                            msg = e.Message

                        });
                    }
                    return Ok(
                        new {
                            success=1,
                            userDorm= userDorm
                        }
                        );
                }

            }
            else return Ok(new {
                success =0,
                msg="Not Authorized"
            });
        }
        [HttpPut]
        public IActionResult ChangeDorm([FromBody] UserDorm userDorm)
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            User user = DBContext.DBstatic.Queryable<User>().Single(c => c.UserID == userID);
            int dormNum;
            int dormMaxNum;
            try
            {
                user = UserMapper.GetUserByID(userID);
                dormNum = UserDormMapper.GetDormNum(userDorm.DormID);
                dormMaxNum = DormMapper.GetMaxNum(userDorm.DormID);
            }
            catch (Exception e) { return Ok(new { success = 0, msg = e.Message }); }
            if (user == null) return Ok(new { success = 0, msg = "No this user" });
            else if (user.Access == 1)
            {
                if (dormNum >= dormMaxNum)
                {
                    return Ok(new { success = 0, msg = "No more position" });
                }
                else
                {
                    //此处需要修改
                    UserDormMapper.ChangeDorm(userDorm);
                    return Ok(
                        new
                        {
                            success = 1,
                            userDorm = userDorm
                        }
                        );
                }

            }
            else return Ok(new { success = 0, msg = "Not Authorized" });
        }
    }
}
