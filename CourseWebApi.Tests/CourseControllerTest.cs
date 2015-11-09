using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using CourseWebApi.Controllers;

namespace CourseWebApi.Tests
{
    [TestClass]
    public class CourseControllerTest
    {
        [TestMethod]
        public void Post()
        {
            //Arrange
            CourseController course = new CourseController();

            //Act
            List<string> list = new List<string>();
            string response = course.Post(list);

            //Assert
            Assert.IsTrue(response.Length <= 0);
        }
    }
}
