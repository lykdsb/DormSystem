using System;
using SqlSugar;
namespace backend.Models
{
    [SugarTable("Reply")]
    public class Reply
    {
        public int ReplyID { set; get; }
        public int PostID { set; get; }
        public int UserID { set; get; }
        public string Content { set; get; }
        public DateTime ReplyTime { set; get; }

    }
}
