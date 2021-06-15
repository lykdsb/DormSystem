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
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ChangeController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] ApplicationForChanging afc)
        {
            try
            {
                afc.ApplicationDate = DateTime.Today;
                await ChangeMapper.Submit(afc);
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
        [HttpGet]
        public async Task<IActionResult> GetApplications()
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            User user;
            List<ApplicationForChanging> myApplications;
            List<ApplicationForChanging> applicationsNotCompleted;
            try
            {
                user = await UserMapper.GetUserByID(userID);
                
                
            if (user.Access == 0)
            {
                    myApplications = await ChangeMapper.GetMine(userID);
                    return Ok(new
                {
                    success = 1,
                    applications = myApplications
                });
            }
            else
            {
                    applicationsNotCompleted = await ChangeMapper.GetNotCompleted();
                    return Ok(new
                {
                    success = 1,
                    applicaitons = applicationsNotCompleted
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
        [HttpPatch("{applicationID}")]
        public async Task<IActionResult> Deal(int applicationID)
        {
            try
            {
                await ChangeMapper.Deal(applicationID);
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
