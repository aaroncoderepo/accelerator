using School.Domain.Entities;
using School.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Site.Services.Impl
{
    public class SchoolService : ISchoolService
    {
        private IClassRepository _classRepository;
        private IStudentRepository _studentRepository;

        public SchoolService(IClassRepository classRepository, IStudentRepository studentRepository)
        {
            _classRepository = classRepository;
            _studentRepository = studentRepository;
        }

        public bool CreateClass(string className, string location, string teacherName)
        {
            Class schoolClass = new Class();
            schoolClass.Name = className;
            schoolClass.Location = location;
            schoolClass.TeacherName = teacherName;
            schoolClass.CreatedDate = DateTime.Now;

            return _classRepository.CreateClass(schoolClass);
        }
        public void DeleteClass(int ClassId)
        {
            _classRepository.DeleteClass(ClassId);
        }

        public Class GetClass(int Id)
        {
            return _classRepository.GetClass(Id);
        }

        public List<Class> GetSchoolClasses()
        {
            return _classRepository.GetSchoolClasses();
        }

        public List<Student> GetStudentsByClass(int ClassId)
        {
            return _classRepository.GetStudentsByClass(ClassId);
        }

        public void EditClass(int ClassId, string className, string location, string teacherName)
        {
            _classRepository.EditClass(ClassId, className, location, teacherName);
        }

        public bool CreateStudent(int ClassId, string studentName, int studentAge, double studentGPA)
        {
            string surname = studentName.Trim().Split(' ').LastOrDefault().Trim();

            Student studentLookup = _studentRepository.GetStudentBySurname(surname);

            if (studentLookup != null)
                return false;

            Student student = new Student();
            student.Name = studentName;
            student.Age = studentAge;
            student.GPA = studentGPA;
            student.CreatedDate = DateTime.Now;

            return _studentRepository.CreateStudent(ClassId, student);
        }

        public void DeleteStudent(int StudentId)
        {
            _studentRepository.DeleteStudent(StudentId);
        }

        public void EditStudent(int StudentId, string studentName, int studentAge, double studentGPA)
        {
            _studentRepository.EditStudent(StudentId, studentName, studentAge, studentGPA);
        }
    }
}