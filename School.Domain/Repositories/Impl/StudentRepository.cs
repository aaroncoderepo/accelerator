using School.Domain.DBContext;
using School.Domain.Entities;
using School.Domain.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Repositories.Impl
{
    public class StudentRepository : IStudentRepository
    {
        private SchoolDbContext _dbContext;

        public StudentRepository(SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Student GetStudent(int id)
        {
            try
            {
                Student student = _dbContext.Student.SingleOrDefault(s => s.Id == id);

                return student;
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
                return null;
            }
        }

        public Student GetStudentBySurname(string surname)
        {
            try
            {
                Student student = _dbContext.Student.SingleOrDefault(s => s.Name.EndsWith(surname));

                return student;
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
                return null;
            }
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


        public bool CreateStudent(int ClassId, Student student)
        {
            try
            {
                Class studentClass = GetClass(ClassId);

                if (studentClass == null)
                    return false;

                student.Class = studentClass;

                _dbContext.Student.Add(student);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
                return false;
            }
        }

        public void DeleteStudent(int StudentId)
        {
            var student = GetStudent(StudentId);

            if (student == null)
                return;

            _dbContext.Student.Remove(student);
            _dbContext.SaveChanges();
        }

        public void EditStudent(int StudentId, string studentName, int studentAge, double studentGPA)
        {
            try
            {
                var student = GetStudent(StudentId);

                if (student == null)
                    return;

                student.Name = studentName;
                student.Age = studentAge;
                student.GPA = studentGPA;

                _dbContext.Entry(student).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
            }
        }
    }
}
