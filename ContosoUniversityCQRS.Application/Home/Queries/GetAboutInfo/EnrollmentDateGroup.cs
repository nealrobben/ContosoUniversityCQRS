using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversityCQRS.Application.Home.Queries.GetAboutInfo
{
    public class EnrollmentDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? EnrollmentDate { get; set; }

        public int StudentCount { get; set; }
    }
}
