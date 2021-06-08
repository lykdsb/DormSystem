using System;
using SqlSugar;
namespace backend.Configs
{
    public class DBContext
    {
        public static SqlSugarClient DBstatic
        {
            get => new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = GlobalVar.ConnectString,
                DbType = DbType.MySql,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true,
            });
        }
    }
}
