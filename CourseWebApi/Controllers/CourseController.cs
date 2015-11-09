using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CourseWebApi.Controllers
{
    public class CourseController : ApiController
    {
        // POST api/values
        public String Post([FromBody]List<string> inputs)
        {
            if (inputs != null)
            {

                return String.Join(",", inputs);
            }
            else
            {
                return "Nothing received";
            }
        }
    }
}
