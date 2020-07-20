namespace ContosoUniversityCQRS.Application.Courses.Queries.DeleteConfirmation
{
    public class DeleteCourseVM
    {
        public int CourseID { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public string DepartmentName { get; set; }
    }
}
