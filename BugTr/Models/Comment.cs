using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace BugTr.Models
{
    public class Comment
    {
        [ForeignKey("Id")]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        // Foreign keys
        
       

        // Navigation properties
        public int UserId { get; set; }
        public User User { get; set; }
        public int BugId { get; set; }
        public Bug Bug { get; set; }
    }
}
