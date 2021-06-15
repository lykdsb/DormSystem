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
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class DormController:ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetDormInfo()
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            User user;
            List<UserDorm> userDorms;
            List<UserDorm> roomates;
            int dormID;
            try
            {
                user = await UserMapper.GetUserByID(userID);
               
            if (user.Access == 1)
            {
                    userDorms = await UserDormMapper.GetUserDorms();
                    return Ok(new
                {
                    success = 1,
                    dormList = userDorms
                }
                 );
            }
            else
            {
                dormID = await UserDormMapper.GetDormID(userID);
                roomates = await UserDormMapper.GetRoomates(dormID);
                List<int> roomateID = new List<int>();
                foreach (UserDorm ud in roomates)
                {
                    roomateID.Add(ud.UserID);
                }
                return Ok(
                    new
                    {
                        success = 1,
                        dormID = dormID,
                        roomateID = roomateID
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
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> ArrangeDorm([FromBody] UserDorm userDorm)
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            User user;
            int dormNum;
            int dormMaxNum;
            try
            {
                user = await UserMapper.GetUserByID(userID);
            if (user.Access == 1)
            {
                    dormNum = await UserDormMapper.GetDormNum(userDorm.DormID);
                    dormMaxNum = await DormMapper.GetMaxNum(userDorm.DormID);
                    if (dormNum >= dormMaxNum)
                {
                    return Ok(new
                    {
                        success = 0,
                        msg = "No more position"
                    });
                }
                else
                {

                    await UserDormMapper.ArrangeDorm(userDorm);
                    return Ok(
                        new {
                            success = 1,
                            userDorm = userDorm
                        }
                        );
                }

            }
            else return Ok(new {
                success = 0,
                msg = "Not Authorized"
            });
            }

            catch (Exception e)
            {
                return Ok(new
                {
                    success = 0,
                    msg = e.Message
                });
            }
        }
        [HttpPut]
        public async Task<IActionResult> ChangeDorm([FromBody] UserDorm userDorm)
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            User user = DBContext.DBstatic.Queryable<User>().Single(c => c.UserID == userID);
            int dormNum;
            int dormMaxNum;
            try
            {
                user = await UserMapper.GetUserByID(userID);

            if (user.Access == 1)
            {
                    dormNum = await UserDormMapper.GetDormNum(userDorm.DormID);
                    dormMaxNum = await DormMapper.GetMaxNum(userDorm.DormID);
                    if (dormNum >= dormMaxNum)
                {
                    return Ok(new { success = 0, msg = "No more position" });
                }
                else
                {
                    await UserDormMapper.ChangeDorm(userDorm);
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
            catch (Exception e) { return Ok(new { success = 0, msg = e.Message }); }
        }
    }
}
