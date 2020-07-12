using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;

namespace ContosoUniversityCQRS.Application.Home.Queries.GetAboutInfo
{
    public class AboutInfoVM
    {
        public List<EnrollmentDateGroup> Items { get; }

        public AboutInfoVM(List<EnrollmentDateGroup> items)
        {
            Items = items;
        }
    }
}
