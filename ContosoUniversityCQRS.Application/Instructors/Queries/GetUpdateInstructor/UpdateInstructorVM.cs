using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.GetUpdateInstructor
{
    public class UpdateInstructorVM
    {
        public int InstructorID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        public string OfficeLocation { get; set; }
    }
}
