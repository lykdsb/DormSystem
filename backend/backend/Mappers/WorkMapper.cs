using backend.Configs;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Mappers
{
    public class WorkMapper
    {
        public static async Task<Work> GetWorkByID(int workID)
        {
            Work work;
            try
            {
                work = await DBContext.DBstatic.Queryable<Work>().SingleAsync(c => c.WorkID == workID);
                if (work == null)
                    throw new Exception("No this work");
            }
            catch (Exception e)
            {
                throw e;
            }
            return work;

        }
        public static async Task ArrageWork(Work work)
        {
            try
            {
                await DBContext.DBstatic.Insertable<Work>(work).ExecuteCommandAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task<List<Work>> GetWorks(int userID)
        {
            List<Work> works;
            try
            {
                works = await DBContext.DBstatic.Queryable<Work>().Where(c => c.UserID == userID).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return works;
        }
        public static async Task Done(int workID)
        {
            Work work;
            try
            {
                work = await GetWorkByID(workID);
                if (work.IsCompleted == 1)
                {
                    throw new Exception("This work is completed");
                }
                else
                {
                    work.IsCompleted = 1;
                    await DBContext.DBstatic.Updateable<Work>(work).ExecuteCommandAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
