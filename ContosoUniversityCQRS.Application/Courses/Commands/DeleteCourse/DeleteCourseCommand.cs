﻿using MediatR;

namespace ContosoUniversityCQRS.Application.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommand : IRequest
    {
        public int ID { get; set; }

        public DeleteCourseCommand(int id)
        {
            ID = id;
        }
    }
}
