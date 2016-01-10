using System;
using System.Collections.Generic;
using System.Linq; 
using System.Web;
using System.Web.Mvc;
using School.Domain.Repositories;
using School.Domain.Entities;
using School.Site.Services;

namespace School.Site.Controllers
{
    public class HomeController : Controller
    {
        private ISchoolService _schoolService;

        public HomeController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        public ActionResult Index()
        {
            List<Class> classes = _schoolService.GetSchoolClasses();

            return View(classes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClass(string className, string location, string teacherName)
        {
            bool status = _schoolService.CreateClass(className, location, teacherName);

            return Json(new { success = status });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudent(int classId, string studentName, int studentAge, double studentGPA)
        {
            bool status = _schoolService.CreateStudent(classId, studentName, studentAge, studentGPA);

            return Json(new { success = status });
        }
    }
}