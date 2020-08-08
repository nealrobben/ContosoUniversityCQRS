using AutoMapper;
using ContosoUniversityCQRS.Application.Common.Mappings;
using ContosoUniversityCQRS.Domain.Entities;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorsLookup
{
    public class InstructorLookupVM : IMapFrom<Instructor>
    {
        public int ID { get; set; }
        public string FullName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Instructor, InstructorLookupVM>();
        }
    }
}
