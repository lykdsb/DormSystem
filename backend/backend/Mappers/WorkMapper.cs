using backend.Configs;
using backend.Models;
using System;
using System.Collections.Generic;
namespace backend.Mappers
{
    public class WorkMapper
    {
        public static Work GetWorkByID(int workID)
        {
            Work work;
            try
            {
                work = DBContext.DBstatic.Queryable<Work>().Single(c => c.WorkID == workID);
                if (work == null)
                    throw new Exception("No this work");
            }
            catch (Exception e)
            {
                throw e;
            }
            return work;

        }
        public static void ArrageWork(Work work)
        {
            try
            {
                DBContext.DBstatic.Insertable<Work>(work).ExecuteCommand();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static List<Work> GetWorks(int userID)
        {
            List<Work> works;
            try
            {
                works = DBContext.DBstatic.Queryable<Work>().Where(c => c.UserID == userID).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return works;
        }
        public static void Done(int workID)
        {
            Work work;
            try
            {
                work = GetWorkByID(workID);
                if (work.IsCompleted == 1)
                {
                    throw new Exception("This work is completed");
                }
                else
                {
                    work.IsCompleted = 1;
                    DBContext.DBstatic.Updateable<Work>(work).ExecuteCommand();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
