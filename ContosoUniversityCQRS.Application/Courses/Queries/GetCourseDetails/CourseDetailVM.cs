﻿using AutoMapper;
using ContosoUniversityCQRS.Application.Common.Mappings;
using ContosoUniversityCQRS.Domain.Entities;

namespace ContosoUniversityCQRS.Application.Courses.Queries.GetCourseDetails
{
    public class CourseDetailVM : IMapFrom<Course>
    {
        public int CourseID { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public int DepartmentID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, CourseDetailVM>();
        }
    }
}
