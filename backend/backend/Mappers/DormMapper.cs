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
            Dorm dorm;
            try
            {
                dorm = DBContext.DBstatic.Queryable<Dorm>().Single(c => c.DormID == dormID);
                if (dorm == null) throw new Exception("No this dorm");
                maxNum = dorm.MaxNum;
            }
            catch (Exception e)
            {
                throw e;
            }
            return maxNum;
        }
    }
}
