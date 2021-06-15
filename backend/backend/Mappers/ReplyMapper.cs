using System;
using backend.Configs;
using backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Mappers
{
    public class ReplyMapper
    {
        public static async Task Reply(Reply reply)
        {
            try
            {
                reply.ReplyTime = DateTime.Now;
                await DBContext.DBstatic.Insertable<Post>(reply).ExecuteCommandAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task<List<Reply>> GetReplys(int postID)
        {
            List<Reply> replys;
            try
            {
                replys = await DBContext.DBstatic.Queryable<Reply>().Where(c => c.PostID ==postID ).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return replys;
        }
    }
}
