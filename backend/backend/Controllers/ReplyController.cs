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
    public class ReplyController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult >Reply([FromBody] Reply reply)
        {
            try
            {
                reply.ReplyTime = DateTime.Now;
                await ReplyMapper.Reply(reply);
                return Ok(new
                {
                    success = 1
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
        [HttpGet("{postID}")]
        public async Task<IActionResult> GetReplys(int postID)
        {
            List<Reply> replys;
            List<string> userNames = new List<string>();
            try
            {
                replys = await ReplyMapper.GetReplys(postID);
                foreach (Reply reply in replys)
                {
                    userNames.Add((await UserMapper.GetUserByID(reply.UserID)).UserName);
                }
                return Ok(new
                {
                    success = 1,
                    replys = replys,
                    userNames= userNames
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
