using System;
namespace backend.Configs
{
    public class GlobalVar:MarshalByRefObject
    {
        //配置数据库连接
        public static string ConnectString = "server=server.lykdsb.cn;database=DormSystem;uid=root;pwd=Mysql_183492765";
        //配置jwt
        public static string Secret = "This is the secret key of the program DormSystem";
        //配置域名
        public static string Domain = "http://localhost:5000";
    }
}
