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
    public class ReplyController : ControllerBase
    {
        [HttpPost]
        public IActionResult Reply([FromBody] Reply reply)
        {
            try
            {
                reply.ReplyTime = DateTime.Now;
                ReplyMapper.Reply(reply);
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
                success = 1
            });
        }
        [HttpGet("{postID}")]
        public IActionResult GetReplys(int postID)
        {
            List<Reply> replys;
            try
            {
                replys = ReplyMapper.GetReplys(postID);
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
                replys = replys
            });
        }
    }
}
