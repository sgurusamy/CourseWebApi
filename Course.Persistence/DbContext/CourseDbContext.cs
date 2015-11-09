using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Data.Entity;
using DE = Course.DataEntities;


namespace Course.Persistence
{
    public class CourseDbContext : DbContext
    {

        public DbSet<DE.Course> Courses { get; set; }
        public DbSet<DE.PrerequisiteCourse> PrerequisiteCourses { get; set; }

        static CourseDbContext()
        {
            Database.SetInitializer<CourseDbContext>(null);
        }

        internal CourseDbContext()
            : base("Course.Database")
        {
            Database.Initialize(false);
        }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            MapAddressObject(modelBuilder);
        }

        private void MapAddressObject(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DE.Course>().ToTable("Course");
            modelBuilder.Entity<DE.Course>().HasKey(s => s.CourseID);
            modelBuilder.Entity<DE.Course>().Property(s => s.Description);

            modelBuilder.Entity<DE.PrerequisiteCourse>().ToTable("PrerequisiteCourse");
            modelBuilder.Entity<DE.PrerequisiteCourse>().HasKey(s => s.Id_Course);
            modelBuilder.Entity<DE.PrerequisiteCourse>().Property(s => s.Id_Prereq_Course);
        }
    }
}
