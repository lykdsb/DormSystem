using System;
using SqlSugar;
namespace backend.Models
{
    [SugarTable("ApplicationForChanging")]
    public class ApplicationForChanging
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ApplicationID { get; set; }
        public int UserID { get; set; }
        public int DestDormID { get; set; }
        public string Reason { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int IsCompleted { get; set; }
    }
}
