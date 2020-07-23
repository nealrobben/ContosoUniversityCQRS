using ContosoUniversityCQRS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Courses.Queries.GetCourseList
{
    public class GetCourseListCommandHandler : IRequestHandler<GetCourseListCommand, List<CourseSelectionVM>>
    {
        private readonly ISchoolContext _context;

        public GetCourseListCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<List<CourseSelectionVM>> Handle(GetCourseListCommand request, CancellationToken cancellationToken)
        {
            var allCourses = await _context.Courses.AsNoTracking().ToListAsync();
            var courseIDsForInstructor = await _context.CourseAssignments.Where(x => x.InstructorID == request.InstructorID).Select(x => x.CourseID).ToListAsync();

            var courses = new List<CourseSelectionVM>();

            foreach(var course in allCourses)
            {
                courses.Add(new CourseSelectionVM
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = courseIDsForInstructor.Contains(course.CourseID)
                });
            }

            return courses;
        }
    }
}
