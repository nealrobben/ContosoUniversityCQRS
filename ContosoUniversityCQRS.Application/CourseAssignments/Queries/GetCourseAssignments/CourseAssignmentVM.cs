namespace ContosoUniversityCQRS.Application.CourseAssignments.Queries.GetCourseAssignments
{
    public class CourseAssignmentVM
    {
        public int CourseID { get; set; }

        public string Title { get; set; }

        public bool Assigned { get; set; }
    }
}
