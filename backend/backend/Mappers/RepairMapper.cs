using System;
using backend.Configs;
using backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Mappers
{
    public class RepairMapper
    {

        public static async Task<ApplicationForRepairing> GetAfrByID(int applicationId)
        {
            ApplicationForRepairing afr;
            try
            {
                afr = await DBContext.DBstatic.Queryable<ApplicationForRepairing>().SingleAsync(c => c.ApplicationID == applicationId);
                if (afr == null) throw new Exception("No this application");
            }
            catch (Exception e)
            {
                throw e;
            }
            return afr;
        }

        public static async Task Submit(ApplicationForRepairing afr)
        {
            try
            {
                await DBContext.DBstatic.Insertable<ApplicationForRepairing>(afr).ExecuteCommandAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task Deal(int applicationID)
        {
            ApplicationForRepairing afr;
            try
            {
                afr = await GetAfrByID(applicationID);
                if (afr.IsCompleted == 1)
                {
                    throw new Exception("This application is completed");
                }
                else
                {
                    afr.IsCompleted = 1;
                    await DBContext.DBstatic.Updateable<ApplicationForRepairing>(afr).ExecuteCommandAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static async Task<List<ApplicationForRepairing>> GetNotCompleted()
        {
            List<ApplicationForRepairing>afrs;
            try
            {
                afrs = await DBContext.DBstatic.SqlQueryable<ApplicationForRepairing>("select * from ApplicationForRepairing where isCompleted=1").ToListAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
            return afrs;
        }
        public static async Task<List<ApplicationForRepairing>> GetMine(int userID)
        {
            List<ApplicationForRepairing> afrs;
            try
            {
                afrs = await DBContext.DBstatic.SqlQueryable<ApplicationForRepairing>($"select * from ApplicationForRepairing where userID = {userID}").ToListAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
            return afrs;
        }
        
    }
}
