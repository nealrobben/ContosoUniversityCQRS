﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoUniversityCQRS.Domain.Entities
{
    public class CourseAssignment
    {
        public int InstructorID { get; set; }
        public int CourseID { get; set; }

        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
    }
}
