using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BugTr.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        

        // Navigation properties
        public ICollection<Project> Projects { get; set; }

        public ICollection<Bug> Bugs { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
