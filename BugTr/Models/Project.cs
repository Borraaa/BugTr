using System;
namespace BugTr.Models

{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Foreign keys


        // Navigation properties
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Bug> Bugs { get; set; }
    }
}
