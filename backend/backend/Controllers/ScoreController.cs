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
    public class ScoreController:ControllerBase
    {

        [HttpGet]
        public IActionResult GetScores()
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            User user;
            List<Score> scores;
            List<Score> myScores;
            try
            {
                user = UserMapper.GetUserByID(userID);
                scores = ScoreMapper.GetScores();
                myScores = ScoreMapper.GetMyScores(userID);
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
                    scores = scores
                });
            }
            else
            {
                return Ok(new
                {
                    success = 1,
                    scores = myScores
                });
            }
        }
        [HttpPost]
        public IActionResult Score([FromBody] Score score)
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            User user;
            try
            {
                user = UserMapper.GetUserByID(userID);

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
                    success = 0,
                    msg = "Not Authorized"
                });
            }
            else
            {
                try
                {
                    ScoreMapper.Score(score);
                }
                catch (Exception e)
                {
                    return Ok(new
                    {
                        success = 0,
                        msg = e.Message
                    });
                }
                return Ok(new {
                    success=1,
                });
            }
        }
    }
}
