﻿using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Instructors.Commands.UpdateInstructor
{
    public class UpdateInstructorCommandHandler : IRequestHandler<UpdateInstructorCommand>
    {
        private readonly ISchoolContext _context;

        public UpdateInstructorCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
        {
            if (request.InstructorID == null)
                throw new NotFoundException(nameof(Instructor), request.InstructorID);

            var instructorToUpdate = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                .ThenInclude(i => i.Course)
                .FirstOrDefaultAsync(m => m.ID == request.InstructorID);

            if (instructorToUpdate == null)
                throw new NotFoundException(nameof(Instructor), request.InstructorID);

            instructorToUpdate.LastName = request.LastName;
            instructorToUpdate.FirstMidName = request.FirstName;
            instructorToUpdate.HireDate = request.HireDate;

            if (instructorToUpdate.OfficeAssignment == null)
                instructorToUpdate.OfficeAssignment = new OfficeAssignment();

            instructorToUpdate.OfficeAssignment.Location = request.OfficeLocation;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
