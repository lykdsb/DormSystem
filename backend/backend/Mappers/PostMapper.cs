using System;
using backend.Configs;
using backend.Models;
using System.Collections.Generic;
namespace backend.Mappers
{
    public class PostMapper
    {
        public static void Post(Post post)
        {
            try
            {
                post.PostTime = DateTime.Now;
                DBContext.DBstatic.Insertable<Post>(post).ExecuteCommand();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static List<Post> GetPosts()
        {
            List<Post> posts;
            try
            {
                posts = DBContext.DBstatic.Queryable<Post>().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return posts;
        }
    }
}
