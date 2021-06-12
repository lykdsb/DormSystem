using System;
using backend.Configs;
using backend.Models;
using System.Collections.Generic;
namespace backend.Mappers
{
    public class ReplyMapper
    {
        public static void Reply(Reply reply)
        {
            try
            {
                reply.ReplyTime = DateTime.Now;
                DBContext.DBstatic.Insertable<Post>(reply).ExecuteCommand();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static List<Reply> GetReplys(int postID)
        {
            List<Reply> replys;
            try
            {
                replys = DBContext.DBstatic.Queryable<Reply>().Where(c => c.PostID ==postID ).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return replys;
        }
    }
}
