using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversityCQRS.Application.Departments.Queries.GetUpdateDepartment
{
    public class UpdateDepartmentVM
    {
        public int DepartmentID { get; set; }

        public string Name { get; set; }

        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        public byte[] RowVersion { get; set; }

        public int? InstructorID { get; set; }
    }
}
