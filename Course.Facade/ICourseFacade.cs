﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Facade
{
    interface ICourseFacade
    {
        string OrderCourses(List<string> courseList);
    }
}
