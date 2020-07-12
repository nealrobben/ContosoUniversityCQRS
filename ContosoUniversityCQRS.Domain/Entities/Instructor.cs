using System;
using System.Collections.Generic;

namespace ContosoUniversityCQRS.Domain.Entities
{
    public class Instructor : Person
    {
        public Instructor()
        {
            CourseAssignments = new HashSet<CourseAssignment>();
            Departments = new HashSet<Department>();
        }

        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime HireDate { get; set; }

        public OfficeAssignment OfficeAssignment { get; set; }
        public ICollection<CourseAssignment> CourseAssignments { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
