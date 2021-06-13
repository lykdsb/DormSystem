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

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ChangeController : ControllerBase
    {
        [HttpPost]
        public IActionResult Submit([FromBody] ApplicationForChanging afc)
        {
            try
            {
                afc.ApplicationDate = DateTime.Today;
                ChangeMapper.Submit(afc);
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
        [HttpGet]
        public IActionResult GetApplications()
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            User user;
            List<ApplicationForChanging> myApplications;
            List<ApplicationForChanging> applicationsNotCompleted;
            try
            {
                user = UserMapper.GetUserByID(userID);
                myApplications = ChangeMapper.GetMine(userID);
                applicationsNotCompleted = ChangeMapper.GetNotCompleted();

            }
            catch (Exception e)
            {
                return Ok(new
                {
                    success = 0,
                    msg = e.Message
                });
            }
            if (user.Access == 0)
            {
                return Ok(new
                {
                    success = 1,
                    applications = myApplications
                });
            }
            else
            {
                return Ok(new
                {
                    success = 1,
                    applicaitons = applicationsNotCompleted
                });
            }
        }
        [HttpPatch]
        public IActionResult Deal(int applicationID)
        {
            try
            {
                ChangeMapper.Deal(applicationID);
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
