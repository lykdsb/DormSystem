using System;
using SqlSugar;
namespace backend.Models
{
    [SugarTable("UserDorm")]
    public class UserDorm
    {
        public int IsLeader { get; set; }
        public int UserId { get; set; }
        public int DormID { get; set; }
    }
}
