namespace ContosoUniversityCQRS.Application.Courses.Queries.GetCourseList
{
    public class CourseSelectionVM
    {
        public int CourseID { get; set; }

        public string Title { get; set; }

        public bool Assigned { get; set; }
    }
}
