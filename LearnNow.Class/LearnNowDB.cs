using LearnNow.Class.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNow.Class
{
    public class LearnNowDB:IdentityDbContext<IdentityUser>
    {
        public DbSet<CourseModel> Courses { get; set; }

        public DbSet<LocationModel> Locations { get; set; }

        public DbSet<TeacherModel> Teachers { get; set; }

        public DbSet<TraineeModel> Trainees { get; set; }
        public DbSet<TrainingEventModel> TrainingEvents { get; set; }
        public LearnNowDB(DbContextOptions<LearnNowDB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CourseModel>()
                .Property("Price")
                .HasPrecision(18, 2);
            builder.Entity<TeacherModel>()
                .HasIndex(t => t.Email)
                .IsUnique();
            builder.Entity<TraineeModel>()
                .HasIndex(t=> t.Email)
                .IsUnique();
            base.OnModelCreating(builder);
        }
    }
}
