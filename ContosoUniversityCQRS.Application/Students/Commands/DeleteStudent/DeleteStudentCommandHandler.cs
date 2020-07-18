using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
    {
        private readonly ISchoolContext _context;

        public DeleteStudentCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.FindAsync(request.ID);

            if (student == null)
                throw new NotFoundException(nameof(Student), request.ID);

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
