using backend.Configs;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Mappers
{
    public class UserMapper
    {
        public static async Task<User> GetUserByID(int userID)
        {
            User user;
            try
            {
                user = await DBContext.DBstatic.Queryable<User>().SingleAsync(c => c.UserID == userID);
                if (user == null) throw new Exception("No this user");
            }
            catch (Exception e)
            {
                throw e;
            }
            return user;
        }
        public static async Task<int> AddUser(User user)
        {
            int userID;
            try
            {
                await DBContext.DBstatic.Insertable<User>(user).ExecuteCommandAsync();
                userID = await DBContext.DBstatic.Queryable<User>().MaxAsync<int>("userID");
            }
            catch (Exception e)
            {
                throw e;
            }
            return userID;
        }
        public static async Task<List<User>> GetUsers()
        {
            List<User> users;
            try
            {
                users = await DBContext.DBstatic.Queryable<User>().ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return users;
        }
        public static async Task<List<User>> GetUsersIsNotArranged()
        {
            List<User> users;
            try
            {
                users =  await DBContext.DBstatic.SqlQueryable<User>($"select * from User where userID not in (select userID from UserDorm)").ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return users;
        }
    }
}
