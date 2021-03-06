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
    public class PostController:ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Post post)
        {
            try
            {
                post.PostTime = DateTime.Now;
                await PostMapper.Post(post);
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
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            List<Post> posts;
            List<string> userNames=new List<string>();
            try
            {
                posts = await PostMapper.GetPosts();
                foreach (Post post in posts)
                {
                    userNames.Add((await UserMapper.GetUserByID(post.UserID)).UserName);
                }
                return Ok(new
                {
                    success = 1,
                    posts = posts,
                    userNames = userNames
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
