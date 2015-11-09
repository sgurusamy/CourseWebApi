using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using CourseWebApi.Controllers;

namespace CourseWebApi.Tests
{
    [TestClass]
    public class CourseControllerTest
    {
        CourseController course = null;

        [TestInitialize]
        public void TestInitialize()
        {
            course = new CourseController();
            course.Request = new HttpRequestMessage();
            course.Configuration = new HttpConfiguration();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            course.Dispose();
            course = null;
        }

        [TestMethod]
        public void Test_For_Null_Course_List()
        {
            //Arrange
            
            //Act
            HttpResponseMessage response = course.Post(null);            
            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void Test_For_Empty_Course_List()
        {
            //Arrange

            //Act
            List<string> list = new List<string>();
            HttpResponseMessage response = course.Post(list);
            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }




    }
}
