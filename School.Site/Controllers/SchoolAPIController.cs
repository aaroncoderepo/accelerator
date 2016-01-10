using School.Domain.Entities;
using School.Site.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace School.Site.Controllers
{
    public class SchoolAPIController : ApiController
    {
        private ISchoolService _schoolService;

        public SchoolAPIController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        // GET api/schoolapi/
        [HttpGet]
        public IEnumerable<Class> GetSchoolClasses()
        {
            List<Class> classes = _schoolService.GetSchoolClasses(); //.Select(_modelFactory.CreateClassModel);

            return classes;
        }

        [HttpGet]
        public IEnumerable<Student> GetStudentsByClass(int id)
        {
            List<Student> students = _schoolService.GetStudentsByClass(id);

            return students;
        }

        [HttpGet]
        public void DeleteClass(int id)
        {
            _schoolService.DeleteClass(id);
        }

        [HttpGet]
        public void DeleteStudent(int id)
        {
            _schoolService.DeleteStudent(id);
        }
    }
}
