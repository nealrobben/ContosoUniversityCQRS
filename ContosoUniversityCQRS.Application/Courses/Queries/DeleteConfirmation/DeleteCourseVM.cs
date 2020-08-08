﻿using AutoMapper;
using ContosoUniversityCQRS.Application.Common.Mappings;
using ContosoUniversityCQRS.Domain.Entities;

namespace ContosoUniversityCQRS.Application.Courses.Queries.DeleteConfirmation
{
    public class DeleteCourseVM : IMapFrom<Course>
    {
        public int CourseID { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public string DepartmentName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, DeleteCourseVM>()
                .ForMember(d => d.DepartmentName, opt => opt.MapFrom(s => s.Department.Name));
        }
    }
}
