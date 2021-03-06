﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Course.Facade;

namespace CourseWebApi.Controllers
{
    public class CourseController : ApiController
    {
        // POST api/values
        public HttpResponseMessage Post([FromBody]List<string> courseList)
        {
            try
            {
                //input is null.
                if (courseList == null || courseList.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest,"List is null or empty");
                }
                CourseFacade courseFacade = new CourseFacade();
                String response = courseFacade.OrderCourses(courseList);
                HttpResponseMessage responseobj = Request.CreateResponse(HttpStatusCode.Created, response);
                return responseobj;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }

        }
    }
}
