using ContosoUniversityCQRS.Domain.Enums;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorsOverview
{
    public class EnrollmentVM
    {
        public string StudentName { get; set; }

        public Grade? StudentGrade { get; set; }
    }
}
