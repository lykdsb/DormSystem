using System;
using SqlSugar;
namespace backend.Models
{
    [SugarTable("ApplicationForRepairing")]
    public class ApplicationForRepairing
    {
        public int ApplicationID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int IsCompleted { get; set; }
    }
}
