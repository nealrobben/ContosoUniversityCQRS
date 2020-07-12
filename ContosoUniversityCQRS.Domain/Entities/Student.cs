using System;
using System.Collections.Generic;

namespace ContosoUniversityCQRS.Domain.Entities
{
    public class Student : Person
    {
        public Student()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
