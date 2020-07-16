using ContosoUniversityCQRS.Domain.Enums;

namespace ContosoUniversityCQRS.Application.Students.Queries.GetStudentDetails
{
    public class EnrollmentVM
    {
        public string CourseTitle { get; set; }

        public Grade? Grade { get; set; }
    }
}