using System.Collections.Generic;

namespace ContosoUniversityCQRS.Application.Courses.Queries.GetCoursesOverview
{
    public class CoursesOverviewVM
    {
        public IList<CourseVM> Courses { get; }

        public CoursesOverviewVM(IList<CourseVM> courses)
        {
            Courses = courses;
        }
    }
}
