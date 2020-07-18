using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Students.Queries.DeleteConfirmation
{
    public class GetDeleteConfirmationCommandHandler : IRequestHandler<GetDeleteConfirmationCommand, DeleteStudentVM>
    {
        private readonly ISchoolContext _context;

        public GetDeleteConfirmationCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<DeleteStudentVM> Handle(GetDeleteConfirmationCommand request, CancellationToken cancellationToken)
        {
            if (request.ID == null)
                throw new NotFoundException(nameof(Student), request.ID);

            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == request.ID);

            if (student == null)
                throw new NotFoundException(nameof(Student), request.ID);

            return new DeleteStudentVM
            {
                StudentID = student.ID,
                FirstName = student.FirstMidName,
                LastName = student.LastName,
                EnrollmentDate = student.EnrollmentDate
            };
        }
    }
}
