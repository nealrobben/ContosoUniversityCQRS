using MediatR;
using System.Collections.Generic;

namespace ContosoUniversityCQRS.Application.Courses.Queries.GetCourseList
{
    public class GetCourseListCommand : IRequest<List<CourseSelectionVM>>
    {
        public int? InstructorID { get; set; }

        public GetCourseListCommand(int? instructorID)
        {
            InstructorID = instructorID;
        }
    }
}
