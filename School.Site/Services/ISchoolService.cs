using School.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Site.Services
{
    public interface ISchoolService
    {
        Class GetClass(int Id);
        List<Class> GetSchoolClasses();
        List<Student> GetStudentsByClass(int ClassId);
        bool CreateClass(string className, string location, string teacherName);
        void DeleteClass(int ClassId);
        void EditClass(int ClassId, string className, string location, string teacherName);
        bool CreateStudent(int ClassId, string studentName, int studentAge, double studentGPA);
        void DeleteStudent(int StudentId);
        void EditStudent(int StudentId, string studentName, int studentAge, double studentGPA);
    }
}