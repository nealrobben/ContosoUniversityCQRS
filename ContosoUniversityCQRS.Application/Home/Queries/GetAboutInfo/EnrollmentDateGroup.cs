using System;

namespace ContosoUniversityCQRS.Application.Home.Queries.GetAboutInfo
{
    public class EnrollmentDateGroup
    {
        public DateTime? EnrollmentDate { get; set; }

        public int StudentCount { get; set; }
    }
}
