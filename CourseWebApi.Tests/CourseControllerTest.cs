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
            //Arrange
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
            //Act
            HttpResponseMessage response = course.Post(null);            
            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [TestMethod]
        public void Test_For_Empty_Course_List()
        {
           //Arrange
            List<string> list = new List<string>();
            //Act
            HttpResponseMessage response = course.Post(list);
            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [TestMethod]
        public void Test_For_Valid_Course_List()
        {
            //Arrange
            List<string> list = new List<string>();
            list.Add("Advanced Pyrotechnics: Introduction to Fire");
            list.Add("Introduction to Fire: ");
           
            //Act
            HttpResponseMessage httpResponse = course.Post(list);
            string responseString = string.Empty;
            if (httpResponse.Content != null)
            {
                responseString = ((ObjectContent)httpResponse.Content).Value.ToString();
            }

            //Assert
            Assert.AreEqual("Introduction to Fire, Advanced Pyrotechnics", responseString);
        }

        [TestMethod]
        public void Test_For_Valid_Complex_Course_List()
        {
            //Arrange
            List<string> list = new List<string>();
            list.Add("Introduction to Paper Airplanes:");
            list.Add("Advanced Throwing Techniques: Introduction to Paper Airplanes");
            list.Add("History of Cubicle Siege Engines: Rubber Band Catapults 101");
            list.Add("Advanced Office Warfare: History of Cubicle Siege Engines");
            list.Add("Rubber Band Catapults 101: ");
            list.Add("Paper Jet Engines: Introduction to Paper Airplanes");

            //Act
            HttpResponseMessage httpResponse = course.Post(list);
            string responseString = string.Empty;
            if (httpResponse.Content != null)
            {
                responseString = ((ObjectContent)httpResponse.Content).Value.ToString();
            }

            //Assert
            Assert.AreEqual("Introduction to Paper Airplanes, Rubber Band Catapults 101, History of Cubicle Siege Engines, Paper Jet Engines, Advanced Office Warfare, Advanced Throwing Techniques", responseString);

            
        }


        [TestMethod]
        public void Test_For_Circular_Dependency_Course_List()
        {
            //Arrange
            List<string> list = new List<string>();
            list.Add("Intro to Arguing on the Internet: Godwin’s Law");
            list.Add("Understanding Circular Logic: Intro to Arguing on the Internet");
            list.Add("Godwin’s Law: Understanding Circular Logic");

            try
            {
                //Act
                HttpResponseMessage httpResponse = course.Post(list);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is HttpResponseException);
                HttpResponseException httpResponseException = (HttpResponseException)ex;
                string exceptionMessage = ((ObjectContent)httpResponseException.Response.Content).Value.ToString();
                Assert.IsTrue(exceptionMessage.Contains("circular dependency exists because of pre requisite"));
            }
        }
    }
}
