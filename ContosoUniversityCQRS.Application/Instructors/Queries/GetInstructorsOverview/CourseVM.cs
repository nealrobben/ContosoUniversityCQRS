using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorsOverview
{
    public class CourseVM
    {
        public int CourseID { get; set; }

        public string Title { get; set; }

        public string DepartmentName { get; set; }
    }
}
