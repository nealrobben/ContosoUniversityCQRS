using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversityCQRS.Application.Departments.Queries.DeleteConfirmation
{
    public class DeleteDepartmentVM
    {
        public int DepartmentID { get; set; }

        public string Name { get; set; }

        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        public byte[] RowVersion { get; set; }

        public string AdministratorName { get; set; }
    }
}
