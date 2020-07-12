﻿using ContosoUniversityCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversityCQRS.Application.Common.Interfaces
{
    public interface ISchoolContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }

        int SaveChanges();
    }
}
