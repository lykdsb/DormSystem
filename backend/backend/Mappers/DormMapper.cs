using System;
using backend.Configs;
using backend.Models;
using System.Collections.Generic;
namespace backend.Mappers
{
    public class DormMapper
    {
        public static int GetMaxNum(int dormID)
        {
            int maxNum;
            try
            {
                maxNum = DBContext.DBstatic.Queryable<Dorm>().Single(c => c.DormID == dormID).MaxNum;
            }
            catch (Exception e)
            {
                throw e;
            }
            return maxNum;
        }
    }
}
