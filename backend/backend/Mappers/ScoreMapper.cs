using System;
using backend.Configs;
using backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Mappers
{
    public class ScoreMapper
    {
        public static async Task Score(Score score)
        {
            try
            {
                await DBContext.DBstatic.Insertable<Post>(score).ExecuteCommandAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task<List<Score>> GetScores()
        {
            List<Score> scores;
            try
            {
                scores = await DBContext.DBstatic.Queryable<Score>().ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return scores;
        }
        public static async Task<List<Score>> GetMyScores(int userID)
        {
            List<Score> scores;
            int dormID;
            try
            {
                dormID = await UserDormMapper.GetDormID(userID);
                scores = await DBContext.DBstatic.Queryable<Score>().Where(c => c.DormID == dormID).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return scores;
        }
        
    }
}
