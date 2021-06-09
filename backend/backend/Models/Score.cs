using System;
using SqlSugar;
namespace backend.Models
{
    [SugarTable("Score")]
    public class Score
    {
        public int DormID { set; get; }
        public int WeekNum { set; get; }
        public int Scores { set; get; }
    }
}
