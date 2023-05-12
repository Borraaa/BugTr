using System;
using BugTr.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTr
{
    public class BugTrackerContext : DbContext
    {
        
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }
        

        public BugTrackerContext(DbContextOptions<BugTrackerContext> options)
           : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL(@"server=localhost;database=CG;user=root;password=Coskun.12345");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>();
            modelBuilder.Entity<Project>();
            modelBuilder.Entity<Bug>();
            modelBuilder.Entity<Comment>();

           

            modelBuilder.Entity<Bug>()
                .Property(b => b.CreatedAt)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<User>()
                 .HasIndex(u => u.Email)
                 .IsUnique();

            modelBuilder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Bug>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bugs)
                .HasForeignKey(b => b.UserId);
            //OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Bug>()
                .HasOne(b => b.Project)
                .WithMany(p => p.Bugs)
                .HasForeignKey(b => b.ProjectId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Bug)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.BugId);
            //Seed data burada eklenebilir, örneğin:
            //modelBuilder.Entity<User>().HasData(
            //    new User { Id = 1, Name = "Test User", Password = "test123", Email = "test@example.com", Role = "Admin" });
        }

        //private void OnDelete(DeleteBehavior clientSetNull)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
