using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using School.Site.Services;
using System.Web.Mvc;
using School.Domain.Repositories;
using Moq;
using School.Site.Services.Impl;
using School.Domain.Entities;

namespace School.Site.Tests
{
    [TestClass]
    public class SchoolUnitTest
    {
        private Mock<IClassRepository> _mockRepository;
        private Mock<IStudentRepository> _mockStudentRepository;
        private ISchoolService _schoolService;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IClassRepository>();
            _mockStudentRepository = new Mock<IStudentRepository>();
            _schoolService = new SchoolService(_mockRepository.Object, _mockStudentRepository.Object);
        }

        [TestMethod]
        public void ExistingStudentSurname()
        {
            // Arrange
            var student = new Student { Name= "Bob Beatty", Class = null, Age=11, GPA =3.5, CreatedDate =DateTime.Now };

            _mockStudentRepository.Setup(r => r.GetStudentBySurname("Bob Beatty")).Returns(student);

            var result = _schoolService.CreateStudent(1, "Bob Beatty", 11, 3.5);

            Assert.IsFalse(result);
        }
    }
}