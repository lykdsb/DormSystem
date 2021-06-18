using System;
using backend.Configs;
using backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Mappers
{
    public class ChangeMapper
    {
        public static async Task<ApplicationForChanging> GetAfcByID(int applicationId)
        {
            ApplicationForChanging afc;
            try
            {
                afc = await DBContext.DBstatic.Queryable<ApplicationForChanging>().SingleAsync(c => c.ApplicationID == applicationId);
                if (afc == null) throw new Exception("No this application");
            }
            catch (Exception e)
            {
                throw e;
            }
            return afc;
        }

        public static async Task Submit(ApplicationForChanging afc)
        {
            try
            {
                await DBContext.DBstatic.Insertable<ApplicationForChanging>(afc).ExecuteCommandAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task Deal(int applicationID)
        {
            ApplicationForChanging afc;
            try
            {
                afc = await GetAfcByID(applicationID);
                if (afc.IsCompleted == 1)
                {
                    throw new Exception("This application is completed");
                }
                else
                {
                    afc.IsCompleted = 1;
                    await DBContext.DBstatic.Updateable<ApplicationForRepairing>(afc).ExecuteCommandAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static async Task<List<ApplicationForChanging>> GetNotCompleted()
        {
            List<ApplicationForChanging> afcs;
            try
            {
                afcs = await DBContext.DBstatic.SqlQueryable<ApplicationForChanging>("select * from ApplicationForChanging where isCompleted=0").ToListAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
            return afcs;
        }
        public static async Task<List<ApplicationForChanging>> GetMine(int userID)
        {
            List<ApplicationForChanging> afcs;
            try
            {
                afcs = await DBContext.DBstatic.SqlQueryable<ApplicationForChanging>($"select * from ApplicationForChanging where userID = {userID}").ToListAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
            return afcs;
        }
    }
}
