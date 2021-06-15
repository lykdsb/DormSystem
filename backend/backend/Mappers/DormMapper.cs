using System;
using System.Threading.Tasks;
using backend.Configs;
using backend.Models;

namespace backend.Mappers
{
    public class DormMapper
    {
        public static async Task<int> GetMaxNum(int dormID)
        {
            int maxNum;
            Dorm dorm;
            try
            {
                dorm = await DBContext.DBstatic.Queryable<Dorm>().SingleAsync(c => c.DormID == dormID);
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
