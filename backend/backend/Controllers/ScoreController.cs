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
            if (user.Access == 0)
            {
                    scores = ScoreMapper.GetScores();
                    return Ok(new
                {
                    success = 1,
                    scores = scores
                });
            }
            else
            {
                    myScores = ScoreMapper.GetMyScores(userID);
                    return Ok(new
                {
                    success = 1,
                    scores = myScores
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
        [HttpPost]
        public IActionResult Score([FromBody] Score score)
        {
            var auth = HttpContext.AuthenticateAsync();
            var userID = Convert.ToInt32(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
            User user;
            try
            {
                user = UserMapper.GetUserByID(userID);
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
                ScoreMapper.Score(score);
                return Ok(new {
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
    }
}
