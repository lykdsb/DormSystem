using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Configs;
using backend.Models;

namespace backend.Mappers
{
    public class DormMapper
    {
        /*public static async Task<List<int>> GetAll()
        {
            List<int> IDs;
            try
            {
                IDs = await DBContext.DBstatic.SqlQueryable<ApplicationForChanging>($"select dormID from ApplicationForChanging where userID = {userID}").ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return maxNum;
        }*/
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
