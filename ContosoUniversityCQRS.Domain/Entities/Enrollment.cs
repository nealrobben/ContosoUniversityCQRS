namespace ContosoUniversityCQRS.Domain.Entities
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public int? Grade { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
