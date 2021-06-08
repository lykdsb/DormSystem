using SqlSugar;
using System;
namespace backend.Models
{
    [SugarTable("Post")]
    public class Post
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public DateTime PostTime { get; set; }
    }
}
