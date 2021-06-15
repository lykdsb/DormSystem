using backend.Configs;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Mappers
{
    public class UserDormMapper
    {
        public static async Task<List<UserDorm>> GetUserDorms()
        {
            List<UserDorm> userDorms;
            try
            {
                userDorms = await DBContext.DBstatic.Queryable<UserDorm>().ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return userDorms;
        }
        public static async Task<UserDorm> GetUserDormByUserID(int userID)
        {
            UserDorm userDorm;
            try
            {
                userDorm = await DBContext.DBstatic.Queryable<UserDorm>().SingleAsync(c => c.UserID == userID);
                if (userDorm == null) throw new Exception("No this userDorm");
            }
            catch (Exception e)
            {
                throw e;
            }
            return userDorm;
        }
        public static async Task<int> GetDormID(int userID)
        {
            int dormID;
            UserDorm userDorm;
            try
            {
                userDorm = await GetUserDormByUserID(userID);
                dormID = userDorm.DormID;
            }
            catch (Exception e)
            {
                throw e;
            }
            return dormID;
        }

        public static async Task<List<UserDorm>> GetRoomates(int dormID)
        {
            List<UserDorm> roomates;
            try
            {
                roomates = await DBContext.DBstatic.Queryable<UserDorm>().Where(c => c.DormID == dormID).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return roomates;
        }
        public static async Task<int> GetDormNum(int dormID)
        {
            int num = 0;
            List<UserDorm> userDorms;
            try
            {
                userDorms = await DBContext.DBstatic.SqlQueryable<UserDorm>($"select * from UserDorm where dormID = {dormID}").ToListAsync();
                num = userDorms.Count;
            }
            catch (Exception e)
            {
                throw e;
            }
            return num;
        }
        public static async Task ArrangeDorm(UserDorm userDorm)
        {
            try
            {
                await DBContext.DBstatic.Insertable<UserDorm>(userDorm).ExecuteCommandAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task ChangeDorm(UserDorm userDorm)
        {
            try
            {
                if (userDorm.IsLeader == 1)
                {
                    List<UserDorm> roomates=await GetRoomates(userDorm.DormID);
                    foreach (UserDorm ud in roomates)
                    {
                        if (ud.IsLeader == 1) throw new Exception("There`s already a leader");
                    }
                }
                await DBContext.DBstatic.Updateable<UserDorm>(userDorm).ExecuteCommandAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
