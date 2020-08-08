using AutoMapper;
using ContosoUniversityCQRS.Application.Common.Mappings;
using ContosoUniversityCQRS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.GetUpdateInstructor
{
    public class UpdateInstructorVM : IMapFrom<Instructor>
    {
        public int InstructorID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        public string OfficeLocation { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Instructor, UpdateInstructorVM>()
                .ForMember(d => d.InstructorID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstMidName))
                .ForMember(d => d.OfficeLocation, opt => opt.MapFrom(s => s.OfficeAssignment.Location));
        }
    }
}
