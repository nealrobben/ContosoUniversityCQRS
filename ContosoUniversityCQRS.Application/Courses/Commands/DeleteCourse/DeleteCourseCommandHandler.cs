using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand>
    {
        private readonly ISchoolContext _context;

        public DeleteCourseCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.FindAsync(request.ID);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.ID);

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
