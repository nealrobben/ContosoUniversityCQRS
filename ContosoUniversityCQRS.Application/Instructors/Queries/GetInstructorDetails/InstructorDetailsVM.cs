using AutoMapper;
using ContosoUniversityCQRS.Application.Common.Mappings;
using ContosoUniversityCQRS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorDetails
{
    public class InstructorDetailsVM : IMapFrom<Instructor>
    {
        public int InstructorID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Instructor, InstructorDetailsVM>()
                .ForMember(d => d.InstructorID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstMidName));
        }
    }
}
