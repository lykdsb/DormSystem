using backend.Configs;
using backend.Models;
using System;
using System.Collections.Generic;
namespace backend.Mappers
{
    public class UserMapper
    {
        public static User GetUserByID(int userID)
        {
            User user;
            try
            {
                user = DBContext.DBstatic.Queryable<User>().Single(c => c.UserID == userID);
            }
            catch (Exception e)
            {
                throw e;
            }
            return user;
        }
        public static int addUser(User user)
        {
            int userID;
            try
            {
                DBContext.DBstatic.Insertable<User>(user).ExecuteCommand();
                userID = DBContext.DBstatic.Queryable<User>().Max<int>("userID");
            }
            catch (Exception e)
            {
                throw e;
            }
            return userID;
        }
        public static List<User> getUsers()
        {
            List<User> users;
            try
            {
                users = DBContext.DBstatic.Queryable<User>().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return users;
        }
    }
}
