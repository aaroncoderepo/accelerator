using School.Domain.Entities;
using School.Domain.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using School.Domain.DBContext;

namespace School.Domain.Repositories.Impl
{
    public class ClassRepository : IClassRepository
    {
        private SchoolDbContext _dbContext;

        public ClassRepository(SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Class GetClass(int id)
        {
            try
            {
                Class SchoolClass = _dbContext.Class.SingleOrDefault(c => c.Id == id);

                return SchoolClass;
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
                return null;
            }
        }

        public List<Class> GetSchoolClasses()
        {
            try
            {
                return _dbContext.Class.ToList();
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
                return null;
            }
        }

        public List<Student> GetStudentsByClass(int ClassId)
        {
            try
            {
                List<Student> students = null;
                
                var schoolClass = _dbContext.Class.Where(c => c.Id == ClassId).Include(b  => b.Students ).SingleOrDefault();

                if (schoolClass != null)
                    students = schoolClass.Students;

                return students;
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
                return null;
            }
        }

        public bool CreateClass(Class schoolClass)
        {
            try
            {
                if (schoolClass == null)
                    return false;

                _dbContext.Class.Add(schoolClass);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
                return false;
            }
        }

        public void DeleteClass(int ClassId)
        {
            try
            {
                var schoolClass = GetClass(ClassId);

                if (schoolClass == null)
                    return;

                foreach (var student in _dbContext.Student.Where(s => s.Class.Id == ClassId))
                {
                    _dbContext.Student.Remove(student);
                }

                schoolClass.Students.Clear();
                _dbContext.Class.Remove(schoolClass);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
            }
        }

        public void EditClass(int ClassId, string className, string location, string teacherName)
        {
            try
            {
                var schoolClass = GetClass(ClassId);

                if (schoolClass == null)
                    return;

                schoolClass.Name = className;
                schoolClass.Location = location;
                schoolClass.TeacherName = teacherName;

                _dbContext.Entry(schoolClass).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
            }
        }
    }
}
