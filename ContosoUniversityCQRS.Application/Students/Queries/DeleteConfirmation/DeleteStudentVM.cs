﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversityCQRS.Application.Students.Queries.DeleteConfirmation
{
    public class DeleteStudentVM
    {
        public int StudentID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }
    }
}
