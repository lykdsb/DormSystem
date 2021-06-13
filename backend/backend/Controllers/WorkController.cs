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
    [Authorize]
    [Route("[controller]")]
    public class WorkController : ControllerBase
    {
        [HttpPost]
        public IActionResult ArrageWork([FromBody] Work work)
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            User user;
            UserDorm userDorm;
            try
            {
                user = UserMapper.GetUserByID(userID);
                userDorm = UserDormMapper.GetUserDormByUserID(userID);
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    success = 0,
                    msg = e.Message
                });
            }
            if (user.Access == 1 || userDorm.IsLeader == 0)
            {
                return Ok(new
                {
                    success = 0,
                    msg = "Not Authorized"
                });
            }
            else
            {
                try
                {
                    WorkMapper.ArrageWork(work);
                }
                catch (Exception e)
                {
                    return Ok(new
                    {
                        success = 0,
                        msg = e.Message
                    });
                }
                return Ok(new
                {
                    success = 1,
                });
            }
        }
        [HttpGet]
        public IActionResult GetMyWorks()
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            User user;
            List<Work> works;
            try
            {
                user = UserMapper.GetUserByID(userID);
                works = WorkMapper.GetWorks(userID);
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    success = 0,
                    msg = e.Message
                });
            }
            return Ok(new
            {
                success = 1,
                works = works
            });
        }
        [HttpPatch("{workID}")]
        public IActionResult Done(int workID)
        {
            try
            {
                WorkMapper.Done(workID);
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    success = 0,
                    msg = e.Message
                });
            }
            return Ok(new
            {
                success = 1,
            });
        }
    }
}
