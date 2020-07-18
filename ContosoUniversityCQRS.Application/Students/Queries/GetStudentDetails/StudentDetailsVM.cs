using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversityCQRS.Application.Students.Queries.GetStudentDetails
{
    public class StudentDetailsVM
    {
        public int StudentID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        public List<EnrollmentVM> Enrollments { get; set; }

        public StudentDetailsVM()
        {
            Enrollments = new List<EnrollmentVM>();
        }
    }
}