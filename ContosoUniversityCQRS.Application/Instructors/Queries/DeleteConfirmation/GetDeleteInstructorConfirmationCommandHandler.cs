using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.DeleteConfirmation
{
    public class GetDeleteInstructorConfirmationCommandHandler : IRequestHandler<GetDeleteInstructorConfirmationCommand, DeleteInstructorVM>
    {
        private readonly ISchoolContext _context;

        public GetDeleteInstructorConfirmationCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<DeleteInstructorVM> Handle(GetDeleteInstructorConfirmationCommand request, CancellationToken cancellationToken)
        {
            if (request.ID == null)
                throw new NotFoundException(nameof(Instructor), request.ID);

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.ID == request.ID);

            if (instructor == null)
                throw new NotFoundException(nameof(Instructor), request.ID);

            return new DeleteInstructorVM
            {
                InstructorID = instructor.ID,
                FirstName = instructor.FirstMidName,
                LastName = instructor.LastName,
                HireDate = instructor.HireDate
            };
        }
    }
}
