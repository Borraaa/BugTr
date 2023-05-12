using System;
using System.ComponentModel.DataAnnotations.Schema;
using BugTr.Models;
namespace BugTr.Models
{
    public class Bug
    {
        

        [ForeignKey("Id")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public BugStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        // Foreign keys
        
        
        public int? BugTypeId { get; set; }
        

        // Navigation properties
        public int UserId { get; set; }
        public User User { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
