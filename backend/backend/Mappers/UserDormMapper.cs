using backend.Configs;
using backend.Models;
using System;
using System.Collections.Generic;
namespace backend.Mappers
{
    public class UserDormMapper
    {
        public static List<UserDorm> GetUserDorms()
        {
            List<UserDorm> userDorms;
            try
            {
                userDorms = DBContext.DBstatic.Queryable<UserDorm>().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return userDorms;
        }
        public static UserDorm GetUserDormByUserID(int userID)
        {
            UserDorm userDorm;
            try
            {
                userDorm = DBContext.DBstatic.Queryable<UserDorm>().Single(c => c.UserID == userID);
                if (userDorm == null) throw new Exception("No this userDorm");
            }
            catch (Exception e)
            {
                throw e;
            }
            return userDorm;
        }
        public static int GetDormID(int userID)
        {
            int dormID;
            try
            {
                dormID = GetUserDormByUserID(userID).DormID;
            }
            catch (Exception e)
            {
                throw e;
            }
            return dormID;
        }

        public static List<UserDorm> GetRoomates(int dormID)
        {
            List<UserDorm> roomates;
            try
            {
                roomates = DBContext.DBstatic.Queryable<UserDorm>().Where(c => c.DormID == dormID).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return roomates;
        }
        public static int GetDormNum(int dormID)
        {
            int num = 0;
            try
            {
                num = DBContext.DBstatic.SqlQueryable<UserDorm>($"select * from userDorm where dormID = {dormID}").ToList().Count;
            }
            catch (Exception e)
            {
                throw e;
            }
            return num;
        }
        public static void ArrangeDorm(UserDorm userDorm)
        {
            try
            {
                DBContext.DBstatic.Insertable<UserDorm>(userDorm).ExecuteCommand();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void ChangeDorm(UserDorm userDorm)
        {
            try
            {
                if (userDorm.IsLeader == 1)
                {
                    List<UserDorm> roomates=GetRoomates(userDorm.DormID);
                    foreach (UserDorm ud in roomates)
                    {
                        if (ud.IsLeader == 1) throw new Exception("There`s already a leader");
                    }
                }
                DBContext.DBstatic.Updateable<UserDorm>(userDorm).ExecuteCommand();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
