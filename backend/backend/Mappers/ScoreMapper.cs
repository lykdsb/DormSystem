using System;
using backend.Configs;
using backend.Models;
using System.Collections.Generic;
namespace backend.Mappers
{
    public class ScoreMapper
    {
        public static void Score(Score score)
        {
            try
            {
                DBContext.DBstatic.Insertable<Post>(score).ExecuteCommand();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static List<Score> GetScores()
        {
            List<Score> scores;
            try
            {
                scores = DBContext.DBstatic.Queryable<Score>().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return scores;
        }
        public static List<Score> GetMyScores(int userID)
        {
            List<Score> scores;
            int dormID;
            try
            {
                dormID = UserDormMapper.GetDormID(userID);
                scores = DBContext.DBstatic.Queryable<Score>().Where(c => c.DormID == dormID).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return scores;
        }
        
    }
}
