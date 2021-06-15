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
    public class RepairController:ControllerBase
    {
        [HttpPost]
        public IActionResult Submit([FromBody]ApplicationForRepairing afr)
        {
            try
            {
                afr.ApplicationDate = DateTime.Today;
                RepairMapper.Submit(afr);
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
        public IActionResult GetApplications()
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            User user;
            List<ApplicationForRepairing> myApplications;
            List<ApplicationForRepairing> applicationsNotCompleted;
            try
            {
                user = UserMapper.GetUserByID(userID);
            if (user.Access == 0)
            {
                    myApplications = RepairMapper.GetMine(userID);
                    return Ok(new
                {
                    success = 1,
                    applications = myApplications
                });
            }
            else {
                    applicationsNotCompleted = RepairMapper.GetNotCompleted();
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
        public IActionResult Deal(int applicationID)
        {
            try
            {
                RepairMapper.Deal(applicationID);
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
