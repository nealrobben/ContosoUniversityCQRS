using AutoMapper;
using ContosoUniversityCQRS.Application.Common.Mappings;
using ContosoUniversityCQRS.Domain.Entities;
using ContosoUniversityCQRS.Domain.Enums;

namespace ContosoUniversityCQRS.Application.Students.Queries.GetStudentDetails
{
    public class EnrollmentVM : IMapFrom<Enrollment>
    {
        public string CourseTitle { get; set; }

        public Grade? Grade { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Enrollment, EnrollmentVM>()
                .ForMember(d => d.CourseTitle, opt => opt.MapFrom(s => s.Course.Title))
                .ForMember(d => d.Grade, opt => opt.MapFrom(s => s.Grade));
        }
    }
}