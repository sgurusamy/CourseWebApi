﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace Course.DataEntities
{
    public class PrerequisiteCourse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Course { get; set; }
        public Nullable<int> Id_Prereq_Course { get; set; }

    }
}
