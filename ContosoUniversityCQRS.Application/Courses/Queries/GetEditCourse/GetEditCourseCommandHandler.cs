using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Courses.Queries.GetEditCourse
{
    public class GetEditCourseCommandHandler : IRequestHandler<GetEditCourseCommand, EditCourseVM>
    {
        private readonly ISchoolContext _context;

        public GetEditCourseCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<EditCourseVM> Handle(GetEditCourseCommand request, CancellationToken cancellationToken)
        {
            if (request.ID == null)
                throw new NotFoundException(nameof(Course), request.ID);

            var course = await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CourseID == request.ID);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.ID);

            return new EditCourseVM
            {
                CourseID = course.CourseID,
                Title = course.Title,
                Credits = course.Credits,
                DepartmentID = course.DepartmentID
            };
        }
    }
}
