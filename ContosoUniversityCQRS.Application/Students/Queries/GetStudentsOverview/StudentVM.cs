using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversityCQRS.Application.Students.Queries.GetStudentsOverview
{
    public class StudentVM
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }
    }
}
