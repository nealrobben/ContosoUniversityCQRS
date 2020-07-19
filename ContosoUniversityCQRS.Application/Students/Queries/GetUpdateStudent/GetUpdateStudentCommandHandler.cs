using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Students.Queries.GetUpdateStudent
{
    public class GetUpdateStudentCommandHandler : IRequestHandler<GetUpdateStudentCommand, UpdateStudentVM>
    {
        private readonly ISchoolContext _context;

        public GetUpdateStudentCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<UpdateStudentVM> Handle(GetUpdateStudentCommand request, CancellationToken cancellationToken)
        {
            if (request.ID == null)
                throw new NotFoundException(nameof(Student), request.ID);

            var student = await _context.Students.FindAsync(request.ID);

            if (student == null)
                throw new NotFoundException(nameof(Student), request.ID);

            return new UpdateStudentVM
            {
                StudentID = student.ID,
                FirstName = student.FirstMidName,
                LastName = student.LastName,
                EnrollmentDate = student.EnrollmentDate
            };
        }
    }
}
