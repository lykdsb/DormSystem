using System;
using SqlSugar;
namespace backend.Models
{
    [SugarTable("Work")]
    public class Work
    {
        public int WorkID { get; set; }
        public int UserID { get; set; }
        public DateTime WorkDate { get; set; }
        public string WorkName { get; set; }
        public int IsCompleted { get; set; }

    }
}
