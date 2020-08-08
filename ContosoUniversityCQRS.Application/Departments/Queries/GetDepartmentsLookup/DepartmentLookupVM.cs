using AutoMapper;
using ContosoUniversityCQRS.Application.Common.Mappings;
using ContosoUniversityCQRS.Domain.Entities;

namespace ContosoUniversityCQRS.Application.Departments.Queries.GetDepartmentsLookup
{
    public class DepartmentLookupVM : IMapFrom<Department>
    {
        public int DepartmentID { get; set; }

        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Department, DepartmentLookupVM>();
        }
    }
}
