using System;
using backend.Configs;
using backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Mappers
{
    public class PostMapper
    {
        public static async Task Post(Post post)
        {
            try
            {
                post.PostTime = DateTime.Now;
                await DBContext.DBstatic.Insertable<Post>(post).ExecuteCommandAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task<List<Post>> GetPosts()
        {
            List<Post> posts;
            try
            {
                posts = await DBContext.DBstatic.Queryable<Post>().ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return posts;
        }
    }
}
