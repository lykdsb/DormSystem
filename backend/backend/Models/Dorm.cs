using SqlSugar;
namespace backend.Models
{
    [SugarTable("Dorm")]
    public class Dorm
    {
        public int DormID { get; set; }
        public string DormName { get; set; }
        public int MaxNum { get; set; }
    }
}
