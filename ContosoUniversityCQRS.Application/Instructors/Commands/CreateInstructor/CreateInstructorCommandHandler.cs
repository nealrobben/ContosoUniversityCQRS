﻿using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Instructors.Commands.CreateInstructor
{
    public class CreateInstructorCommandHandler : IRequestHandler<CreateInstructorCommand>
    {
        private readonly ISchoolContext _context;

        public CreateInstructorCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateInstructorCommand request, CancellationToken cancellationToken)
        {
            _context.Instructors.Add(new Instructor
            {
                FirstMidName = request.FirstName,
                LastName = request.LastName,
                HireDate = request.HireDate
            });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
