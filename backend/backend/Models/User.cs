using SqlSugar;
namespace backend.Models
{
    [SugarTable("User")]
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Access { get; set; }
    }
}
