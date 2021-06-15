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
    [Authorize]
    [Route("[controller]")]
    public class WorkController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ArrageWork([FromBody] Work work)
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            User user;
            UserDorm userDorm;
            try
            {
                user = await UserMapper.GetUserByID(userID);
                userDorm = await UserDormMapper.GetUserDormByUserID(userID);
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
                await WorkMapper.ArrageWork(work);
                return Ok(new
                {
                    success = 1,
                });
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
        [HttpGet]
        public async Task<IActionResult> GetMyWorks()
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            User user;
            List<Work> works;
            try
            {
                user = await UserMapper.GetUserByID(userID);
                works = await WorkMapper.GetWorks(userID);
            return Ok(new
            {
                success = 1,
                works = works
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
        [HttpPatch("{workID}")]
        public async Task<IActionResult> Done(int workID)
        {
            try
            {
            await WorkMapper.Done(workID);
            return Ok(new
            {
                success = 1,
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
    }
}
