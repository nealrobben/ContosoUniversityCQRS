﻿namespace ContosoUniversityCQRS.Application.Courses.Queries.GetCourseDetails
{
    public class CourseDetailVM
    {
        public int CourseID { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public int DepartmentID { get; set; }
    }
}
