using AutoMapper;
using ContosoUniversityCQRS.Application.Common.Mappings;
using ContosoUniversityCQRS.Domain.Entities;
using ContosoUniversityCQRS.Domain.Enums;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorsOverview
{
    public class EnrollmentVM : IMapFrom<Enrollment>
    {
        public string StudentName { get; set; }

        public Grade? StudentGrade { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Enrollment, EnrollmentVM>()
                .ForMember(d => d.StudentName, opt => opt.MapFrom(s => s.Student.FullName))
                .ForMember(d => d.StudentGrade, opt => opt.MapFrom(s => s.Grade));
        }
    }
}
