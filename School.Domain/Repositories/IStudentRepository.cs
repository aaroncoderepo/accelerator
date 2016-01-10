using School.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Repositories
{
    public interface IStudentRepository
    {
        Student GetStudent(int id);
        bool CreateStudent(int ClassId, Student student);
        void DeleteStudent(int StudentId);
        void EditStudent(int StudentId, string studentName, int studentAge, double studentGPA);
        Student GetStudentBySurname(string surname);
    }
}
