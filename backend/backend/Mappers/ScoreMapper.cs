using System;
using backend.Configs;
using backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Mappers
{
    public class ScoreMapper
    {
        public static async Task Score(List<Score> scores)
        {
            try
            {
                foreach(Score score in scores)
                {
                    await DBContext.DBstatic.Insertable<Score>(score).ExecuteCommandAsync();
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task<List<Score>> GetScores(int weekNum)
        {
            List<Score> scores;
            try
            {
                scores = await DBContext.DBstatic.Queryable<Score>().Where(c => c.WeekNum == weekNum).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return scores;
        }
        public static async Task<List<Score>> GetScoresByDormID(int dormID)
        {
            List<Score> scores;
            try
            {
                scores = await DBContext.DBstatic.Queryable<Score>().Where(c => c.DormID == dormID).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return scores;
        }
        public static async Task<List<Score>> GetMyScores(int userID,int weekNum)
        {
            List<Score> scores;
            int dormID;
            try
            {
                dormID = await UserDormMapper.GetDormID(userID);
                scores = await DBContext.DBstatic.Queryable<Score>().Where(c => c.DormID == dormID&&c.WeekNum==weekNum).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return scores;
        }
        
    }
}
