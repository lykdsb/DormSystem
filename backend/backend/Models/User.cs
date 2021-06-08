using SqlSugar;
namespace backend.Models
{
    [SugarTable("User")]
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public int access { get; set; }
    }
}
