using System;
using backend.Configs;
using backend.Models;
using System.Collections.Generic;
namespace backend.Mappers
{
    public class ChangeMapper
    {
        public static ApplicationForChanging GetAfcByID(int applicationId)
        {
            ApplicationForChanging afc;
            try
            {
                afc = DBContext.DBstatic.Queryable<ApplicationForChanging>().Single(c => c.ApplicationID == applicationId);
                if (afc == null) throw new Exception("No this application");
            }
            catch (Exception e)
            {
                throw e;
            }
            return afc;
        }

        public static void Submit(ApplicationForChanging afc)
        {
            try
            {
                DBContext.DBstatic.Insertable<ApplicationForChanging>(afc).ExecuteCommand();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void Deal(int applicationID)
        {
            ApplicationForChanging afc;
            try
            {
                afc = GetAfcByID(applicationID);
                if (afc.IsCompleted == 1)
                {
                    throw new Exception("This application is completed");
                }
                else
                {
                    afc.IsCompleted = 1;
                    DBContext.DBstatic.Updateable<ApplicationForRepairing>(afc).ExecuteCommand();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static List<ApplicationForChanging> GetNotCompleted()
        {
            List<ApplicationForChanging> afcs;
            try
            {
                afcs = DBContext.DBstatic.SqlQueryable<ApplicationForChanging>("select * from ApplicationForChanging where isCompleted=1").ToList();

            }
            catch (Exception e)
            {
                throw e;
            }
            return afcs;
        }
        public static List<ApplicationForChanging> GetMine(int userID)
        {
            List<ApplicationForChanging> afcs;
            try
            {
                afcs = DBContext.DBstatic.SqlQueryable<ApplicationForChanging>($"select * from ApplicationForChanging where userID = {userID}").ToList();

            }
            catch (Exception e)
            {
                throw e;
            }
            return afcs;
        }
    }
}
