using System;
using SqlSugar;
namespace backend.Models
{
    [SugarTable("UserDorm")]
    public class UserDorm
    {
        public int IsLeader { get; set; }

        [SugarColumn(IsPrimaryKey = true,IsIdentity = true)]
        public int UserID { get; set; }

        public int DormID { get; set; }
    }
}
