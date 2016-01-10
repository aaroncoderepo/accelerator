using School.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Repositories
{
    public interface IClassRepository
    {
        Class GetClass(int id);
        List<Class> GetSchoolClasses();
        List<Student> GetStudentsByClass(int ClassId);
        bool CreateClass(Class schoolClass);
        void DeleteClass(int ClassId);
        void EditClass(int ClassId, string className, string location, string teacherName);
    }
}
