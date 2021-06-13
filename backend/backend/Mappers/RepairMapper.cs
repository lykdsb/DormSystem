using System;
using backend.Configs;
using backend.Models;
using System.Collections.Generic;
namespace backend.Mappers
{
    public class RepairMapper
    {

        public static ApplicationForRepairing GetAfrByID(int applicationId)
        {
            ApplicationForRepairing afr;
            try
            {
                afr = DBContext.DBstatic.Queryable<ApplicationForRepairing>().Single(c => c.ApplicationID == applicationId);
                if (afr == null) throw new Exception("No this application");
            }
            catch (Exception e)
            {
                throw e;
            }
            return afr;
        }

        public static void Submit(ApplicationForRepairing afr)
        {
            try
            {
                DBContext.DBstatic.Insertable<ApplicationForRepairing>(afr).ExecuteCommand();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void Deal(int applicationID)
        {
            ApplicationForRepairing afr;
            try
            {
                afr = GetAfrByID(applicationID);
                if (afr.IsCompleted == 1)
                {
                    throw new Exception("This application is completed");
                }
                else
                {
                    afr.IsCompleted = 1;
                    DBContext.DBstatic.Updateable<ApplicationForRepairing>(afr).ExecuteCommand();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static List<ApplicationForRepairing> GetNotCompleted()
        {
            List<ApplicationForRepairing>afrs;
            try
            {
                afrs = DBContext.DBstatic.SqlQueryable<ApplicationForRepairing>("select * from ApplicationForRepairing where isCompleted=1").ToList();

            }
            catch (Exception e)
            {
                throw e;
            }
            return afrs;
        }
        public static List<ApplicationForRepairing> GetMine(int userID)
        {
            List<ApplicationForRepairing> afrs;
            try
            {
                afrs = DBContext.DBstatic.SqlQueryable<ApplicationForRepairing>($"select * from ApplicationForRepairing where userID = {userID}").ToList();

            }
            catch (Exception e)
            {
                throw e;
            }
            return afrs;
        }
        
    }
}
