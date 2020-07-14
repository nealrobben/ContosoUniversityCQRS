﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversityCQRS.Application.Courses.Queries.GetCoursesOverview
{
    public class CourseVM
    {
        [Display(Name = "Number")]
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        public string DepartmentName { get; set; }
    }
}