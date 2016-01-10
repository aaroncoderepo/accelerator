using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Domain.Logging;
using School.Domain.Entities;

namespace School.Domain.DBContext
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new SchoolDbInitializer());
            //Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Class> Class { get; set; }
        public DbSet<Student> Student { get; set; }
    }

    /// <summary>
    /// Database initializer.
    /// </summary>
    public class SchoolDbInitializer : DropCreateDatabaseAlways<SchoolDbContext> // CreateDatabaseIfNotExists
    {
        /// <summary>
        /// Seeds the database with data.
        /// </summary>
        /// <param name="context">the context to seed</param>
        protected override void Seed(SchoolDbContext context)
        {
            try
            {
                Class StudentClassBiology = new Class
                    {
                        Name = "Biology",
                        Location = "Building 5 Room 201",
                        TeacherName = "Mr Robertson",
                        CreatedDate = DateTime.Now
                    };

                List<Student> students = new List<Student>();
                students.Add(new Student { Name = "David Jackson", Age = 19, GPA = 3.4, CreatedDate = DateTime.Now });
                students.Add(new Student { Name = "Peter Parker", Age = 19, GPA = 2.9, CreatedDate = DateTime.Now });
                students.Add(new Student { Name = "Robert Smith", Age = 18, GPA = 3.1, CreatedDate = DateTime.Now });
                students.Add(new Student { Name = "Rebecca Black", Age = 19, GPA = 2.1, CreatedDate = DateTime.Now });

                StudentClassBiology.Students.AddRange(students);

                context.Class.Add(StudentClassBiology);

                Class StudentClassEnglish = new Class
                {
                    Name = "English",
                    Location = "Building 3 Room 104",
                    TeacherName = "Miss Sanderson",
                    CreatedDate = DateTime.Now
                };

                students = new List<Student>();
                students.Add(new Student { Name = "Bob Brown", Age = 18, GPA = 3.6, CreatedDate = DateTime.Now });
                students.Add(new Student { Name = "Mike Taylor", Age = 21, GPA = 2.9, CreatedDate = DateTime.Now });

                StudentClassEnglish.Students.AddRange(students);

                context.Class.Add(StudentClassEnglish);

                context.SaveChanges();
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
            }
        }
    }
}
