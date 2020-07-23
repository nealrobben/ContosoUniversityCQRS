using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.GetUpdateInstructor
{
    class GetUpdateInstructorCommandHandler : IRequestHandler<GetUpdateInstructorCommand, UpdateInstructorVM>
    {
        private readonly ISchoolContext _context;

        public GetUpdateInstructorCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<UpdateInstructorVM> Handle(GetUpdateInstructorCommand request, CancellationToken cancellationToken)
        {
            if (request.ID == null)
                throw new NotFoundException(nameof(Instructor), request.ID);

            var instructor = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments).ThenInclude(i => i.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == request.ID);

            if (instructor == null)
                throw new NotFoundException(nameof(Instructor), request.ID);

            return new UpdateInstructorVM
            {
                InstructorID = instructor.ID,
                LastName = instructor.LastName,
                FirstName = instructor.FirstMidName,
                HireDate = instructor.HireDate,
                OfficeLocation = instructor.OfficeAssignment.Location
            };
        }
    }
}
