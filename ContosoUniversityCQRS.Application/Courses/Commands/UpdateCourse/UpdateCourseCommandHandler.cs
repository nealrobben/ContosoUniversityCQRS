using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand>
    {
        private readonly ISchoolContext _context;

        public UpdateCourseCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            if (request.CourseID == null)
                throw new NotFoundException(nameof(Course), request.CourseID);

            var courseToUpdate = await _context.Courses
                .FirstOrDefaultAsync(c => c.CourseID == request.CourseID);

            courseToUpdate.Title = request.Title;
            courseToUpdate.Credits = request.Credits;
            courseToUpdate.DepartmentID = request.DepartmentID;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
